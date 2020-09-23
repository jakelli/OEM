using System;
using OEM.Helpers;
using Prism.Navigation;
using Xamarin.Forms;

namespace OEM.Pages
{
    public class ScanPageViewModel : BaseViewModel
    {
        public ScanPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Scanner";
        }

        public Command ScanCommand
        {
            get
            {
                return new Command(() =>
                {


                });
            }
        }
    }
}

