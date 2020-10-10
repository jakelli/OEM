using System;
using OEM.Helpers;
using Prism.Navigation;

namespace OEM.Pages
{
    public class PartsPageViewModel : BaseViewModel
    {
        public PartsPageViewModel(INavigationService navigationService)
        : base(navigationService)
        {
            Title = "Part Catalog";
        }
    }
}
