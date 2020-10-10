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

        private BasicVehicleInformation basicVehicleDetails;
        public BasicVehicleInformation BasicVehicleDetails
        {
            get => basicVehicleDetails;
            set => SetProperty(ref basicVehicleDetails, value);
        }

        private ObservableCollection<BasicVehicleInformation> recentSearchResults = new ObservableCollection<BasicVehicleInformation>();
        public ObservableCollection<BasicVehicleInformation> RecentSearchResults
        {
            get => recentSearchResults;
            set
            {
                SetProperty(ref recentSearchResults, value);
            }
        }

        private bool isRecentSearchPopulated;
        public bool IsRecentSearchPopulated
        {
            get => isRecentSearchPopulated;
            set => SetProperty(ref isRecentSearchPopulated, value);
        }

        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set => SetProperty(ref searchTerm, value);
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
        }

        public string ScanIcon => MaterialFontIcon.BarcodeScan;
        public string RecentIcon => MaterialFontIcon.History;

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
                    if (vin == null || vin.ToString() == null || !vin.ToString().IsValidVin())
                    {
                        UserDialogs.Instance.Toast(DialogUtils.GetToastConfig(DialogType.ERROR, "Incomplete VIN, please try again"));
                        SearchTerm = "";
                        return;
                    }

                    IsLoading = true;

                    var vehicleDataResponse = await _vinRepository.GetBasicInformationByVin(vin.ToString());
                    if (vehicleDataResponse.IsSuccess)
                    {
                        BasicVehicleDetails = vehicleDataResponse.BasicVehicleInformation;
                        SearchTerm = vin.ToString().ToUpper();
                        RecentSearchResults.Add(BasicVehicleDetails);

                        if (RecentSearchResults.Count > 5)
                        {
                            RecentSearchResults.RemoveAt(0);
                        }

                        IsRecentSearchPopulated = true;
                    }
                    else
                    {
                        UserDialogs.Instance.Toast(DialogUtils.GetToastConfig(DialogType.ERROR, "No information for " + vin.ToString().ToUpper()));
                        SearchTerm = "";
                    }

                    IsLoading = false;
                });
            }
        }

        public Command OpenPartsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var navigationParameters = new NavigationParameters
                    {
                        { "VehicleDetails", BasicVehicleDetails }
                    };

                    await _navigationService.NavigateAsync("PartsPage");

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
                SearchTerm = "";
                return;
            }

            SearchCommand.Execute(barcodeResult.Text);
        }


    }
}
