using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using OEM.iOS.Effects;
using UIKit;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(ClearEntryEffect), "ClearEntryEffect")]
namespace OEM.iOS.Effects
{
	public class ClearEntryEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			ConfigureControl();
		}

		protected override void OnDetached()
		{
		}

		private void ConfigureControl()
		{
			((UITextField)Control).ClearButtonMode = UITextFieldViewMode.WhileEditing;
		}
	}
}