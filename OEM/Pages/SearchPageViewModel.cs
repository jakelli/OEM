using System.ComponentModel;
using Acr.UserDialogs;
using OEM.Helpers;
using Prism.Navigation;
using Xamarin.Forms;
using ZXing;

namespace OEM.Pages
{
    public class SearchPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;

        public string ScanIcon => MaterialFontIcon.BarcodeScan;

        public SearchPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "OEM+";
        }

        public Command ScanCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigationService.NavigateAsync("ScanPage");

                });
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            var barcodeResult = parameters.GetValue<Result>("BarcodeResult");
            if (barcodeResult != null)
            {
                HandleBarcodeResult(barcodeResult);
            }
        }

        private void HandleBarcodeResult(Result barcodeResult)
        {
            UserDialogs.Instance.Toast("VIN Scanned: " + barcodeResult.Text);
        }
    }
}
