using System;
using OEM.Helpers;
using Prism.Navigation;
using Xamarin.Forms;
using ZXing;

namespace OEM.Pages
{
    public class ScanPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public ScanPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Scanner";
        }

        public Command ScanCommand
        {
            get
            {
                return new Command(async (object result) =>
                {
                    var barcodeResult = (Result)result;
                    if (barcodeResult != null)
                    {
                        var navigationParameters = new NavigationParameters
                        {
                            { "BarcodeResult", result }
                        };
                        await _navigationService.GoBackAsync(navigationParameters);
                    }

                });
            }
        }
    }
}

