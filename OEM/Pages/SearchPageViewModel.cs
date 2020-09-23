using System.ComponentModel;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using OEM.Helpers;
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
            UserDialogs.Instance.ShowLoading();
            var vehicleDataResponse = await _vinRepository.GetBasicInformationByVin("WBA3A9C58FKW74879");
            if (vehicleDataResponse.IsSuccess)
            {
                BasicVehicleDetails = vehicleDataResponse.BasicVehicleInformation.Results;
                //SearchTerm = barcodeResult.Text.ToUpper();
                SearchTerm = "WBA3A9C58FKW74879";
            }
            else
            {
                UserDialogs.Instance.Toast("Invalid VIN");
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}
