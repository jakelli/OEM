using System;
using OEM.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace OEM.Pages
{
    public class MainNavigationPageViewModel : BaseViewModel
    {
        public MainNavigationPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            Title = "OEM+";
        }
    }
}

