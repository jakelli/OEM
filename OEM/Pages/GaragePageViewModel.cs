using System;
using OEM.Helpers;
using Prism.Navigation;

namespace OEM.Pages
{
    public class GaragePageViewModel : BaseViewModel
    {
        public GaragePageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            Title = "Part Catalog";
        }
    }
}
