using System.ComponentModel;
using Acr.UserDialogs;
using OEM.Helpers;
using OEM.Models;
using OEM.Respositories;
using Prism.Navigation;
using Xamarin.Forms;
using ZXing;

namespace OEM.Pages
{
    public class SearchPageViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IVinRepository _vinRepository;

        private BasicVehicleInformation basicVehicle;
        public BasicVehicleInformation BasicVehicle
        {
            get => basicVehicle;
            set => SetProperty(ref basicVehicle, value);
        }

        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set => SetProperty(ref searchTerm, value);
        }

        public string ScanIcon => MaterialFontIcon.BarcodeScan;

        public SearchPageViewModel(INavigationService navigationService, IVinRepository vinRepository)
        : base(navigationService)
        {
            _navigationService = navigationService;
            _vinRepository = vinRepository;
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
            var vehicleDataResponse = _vinRepository.GetBasicInformationByVin(barcodeResult.Text);
            if (vehicleDataResponse.Result.IsSuccess)
            {
                BasicVehicle = vehicleDataResponse.Result.BasicVehicleInformation;
                SearchTerm = barcodeResult.Text.ToUpper();
            }
        }
    }
}
