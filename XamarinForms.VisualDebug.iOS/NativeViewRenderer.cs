using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using XamarinForms.VisualDebug.Core;

[assembly: Dependency(typeof(XamarinForms.VisualDebug.iOS.NativeViewRenderer))]
namespace XamarinForms.VisualDebug.iOS
{
    public class NativeViewRenderer : INativeViewRenderer
    {
        public NativeViewRenderer()
        {
        }

        public byte[] Render(VisualElement formsView)
        {
            var view = ConvertToNative(formsView);

            UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, opaque: true, scale: 0);
            
            UIImage image;

            try
            {
                view.Draw(view.Bounds);

                image = UIGraphics.GetImageFromCurrentImageContext();
            }
            finally
            {
                UIGraphics.EndImageContext();
            }

            NSData imageData = image.AsPNG();

            return imageData.ToArray();
        }

        private UIKit.UIView ConvertToNative(VisualElement view)
        {
            var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(view);
            
            Xamarin.Forms.Platform.iOS.Platform.SetRenderer(view, renderer);

            return renderer.NativeView;
        }
    }
}
