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
            UIView view = ConvertToNative(formsView);

            if (view == null)
            {
                return new byte[0];
            }

            var renderer = new UIGraphicsImageRenderer(view.Bounds.Size);
            UIImage image = renderer.CreateImage((rendererContext) => {
                view.Layer.RenderInContext(rendererContext.CGContext);
            });
                        
            if (image == null)
            {
                return new byte[0];
            }

            NSData imageData = image.AsPNG();

            if (imageData == null)
            {
                return new byte[0];
            }

            return imageData.ToArray();
        }

        private UIKit.UIView ConvertToNative(VisualElement view)
        {
            var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(view);
            
            Xamarin.Forms.Platform.iOS.Platform.SetRenderer(view, renderer);

            return renderer?.NativeView;
        }
    }
}
