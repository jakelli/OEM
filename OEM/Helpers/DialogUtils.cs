using System;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace OEM.Helpers
{
    public static class DialogUtils
    {
        public static ToastConfig GetToastConfig(DialogType dialogType, string message)
        {
            var toastConfig = new ToastConfig(message);

            switch (dialogType)
            {

                case DialogType.WARNING:
                    toastConfig.BackgroundColor = (Color)Application.Current.Resources["AccentYellow2"];
                    toastConfig.MessageTextColor = (Color)Application.Current.Resources["AccentYellow5"];
                    break;

                case DialogType.ERROR:
                    toastConfig.BackgroundColor = (Color)Application.Current.Resources["AccentRed2"];
                    toastConfig.MessageTextColor = (Color)Application.Current.Resources["AccentRed5"];
                    break;
                case DialogType.ALERT:
                    toastConfig.BackgroundColor = Color.White;
                    toastConfig.MessageTextColor = (Color)Application.Current.Resources["NeutralGray8"];
                    break;
                case DialogType.SUCCESS:
                    toastConfig.BackgroundColor = (Color)Application.Current.Resources["PrimaryGreen2"];
                    toastConfig.MessageTextColor = (Color)Application.Current.Resources["PrimaryGreen5"];
                    break;
            }

            return toastConfig;
        }
    }
}
