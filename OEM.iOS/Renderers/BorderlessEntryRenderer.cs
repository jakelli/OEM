using System;
using OEM.Controls;
using OEM.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace OEM.iOS.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                this.Control.BorderStyle = UIKit.UITextBorderStyle.None;
                this.Control.KeyboardType = UIKit.UIKeyboardType.NamePhonePad;
            }
        }
    }
}
