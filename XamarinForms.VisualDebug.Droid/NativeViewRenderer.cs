using System;
using Xamarin.Forms;
using XamarinForms.VisualDebug.Core;

[assembly: Dependency(typeof(XamarinForms.VisualDebug.Droid.NativeViewRenderer))]
namespace XamarinForms.VisualDebug.Droid
{
    public class NativeViewRenderer : INativeViewRenderer
    {
        public NativeViewRenderer()
        {
        }

        public byte[] Render(VisualElement view)
        {
            var viewGroup = ConvertToNative(view);

            var originalCacheEnabledState = viewGroup.DrawingCacheEnabled;

            viewGroup.DrawingCacheEnabled = true;

            var drawingCache = viewGroup.GetDrawingCache(false);

            var image = Android.Graphics.Bitmap.CreateBitmap(drawingCache);

            viewGroup.DrawingCacheEnabled = originalCacheEnabledState;

            using (var stream = new System.IO.MemoryStream())
            {
                image.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);
                var imageBytes = stream.ToArray();
                return imageBytes;
            }
        }

        private Android.Views.View ConvertToNative(VisualElement view)
        {
            var renderer = Xamarin.Forms.Platform.Android.Platform.CreateRendererWithContext(view, Xamarin.Forms.Forms.Context);

            var nativeView = renderer.View;

            renderer.Tracker.UpdateLayout();

            var density = Xamarin.Forms.Forms.Context.Resources.DisplayMetrics.Density;

            var size = new Xamarin.Forms.Rectangle(0, 0, view.Width * density, view.Height * density);

            var layoutParams = new Android.Views.ViewGroup.LayoutParams((int)size.Width, (int)size.Height);

            nativeView.LayoutParameters = layoutParams;

            nativeView.Layout(0, 0, (int)size.Width, (int)size.Height);

            return nativeView;
        }
    }
}
