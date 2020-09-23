using System.ComponentModel;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using OEM.Helpers;
using OEM.Extensions;
using OEM.Models;
using OEM.Webservices.Dtos;
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

        private ObservableCollection<VehicleResult> basicVehicleDetails = new ObservableCollection<VehicleResult>();
        public ObservableCollection<VehicleResult> BasicVehicleDetails
        {
            get => basicVehicleDetails;
            set => SetProperty(ref basicVehicleDetails, value);
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

        public Command SearchCommand
        {
            get
            {
                return new Command(async (object vin) =>
                {
                    UserDialogs.Instance.ShowLoading();

                    var vehicleDataResponse = await _vinRepository.GetBasicInformationByVin(vin.ToString());
                    if (vehicleDataResponse.IsSuccess)
                    {
                        BasicVehicleDetails = vehicleDataResponse.BasicVehicleInformation.Results;
                        SearchTerm = vin.ToString().ToUpper();
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(DialogUtils.GetToastConfig(DialogType.ERROR, "No information for: " + vin.ToString().ToUpper()));
                    }

                    UserDialogs.Instance.HideLoading();

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

        private async void HandleBarcodeResult(Result barcodeResult)
        {
            // ensure the entered value is actually a 17 character VIN
            if (!barcodeResult.Text.IsValidVin())
            {
                UserDialogs.Instance.Toast(DialogUtils.GetToastConfig(DialogType.ERROR, "Incomplete VIN, please try again"));
                return;
            }

            SearchCommand.Execute(barcodeResult.Text);
        }


    }
}
