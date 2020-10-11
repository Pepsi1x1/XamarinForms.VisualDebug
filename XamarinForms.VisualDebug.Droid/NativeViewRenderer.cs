﻿using System;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinForms.VisualDebug.Core;
using View = Android.Views.View;

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
            Context context = Xamarin.Forms.Forms.Context;

            IVisualElementRenderer renderer = Xamarin.Forms.Platform.Android.Platform.CreateRendererWithContext(view, context);

            View nativeView = this.ConvertToNative(context, renderer, view);

            if (view.Width <= 0 || view.Height <= 0)
            {
                return new byte[0];
            }

            if (nativeView.Width <= 0 || nativeView.Height <= 0)
            {
                return new byte[0];
            }

            Bitmap image = this.BitmapFromView(nativeView);

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                image.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 0, stream);

                image.Dispose();

                Xamarin.Forms.Platform.Android.Platform.ClearRenderer(renderer.View);

                renderer.Dispose();

                renderer = null;

                byte[] imageBytes = stream.ToArray();
                
                return imageBytes;
            }
        }

        public Android.Graphics.Bitmap BitmapFromView(Android.Views.View nativeView)
        {
            Android.Graphics.Bitmap bitmap = Android.Graphics.Bitmap.CreateBitmap(nativeView.Width, nativeView.Height, Android.Graphics.Bitmap.Config.Argb8888);

            Android.Graphics.Canvas canvas = new Android.Graphics.Canvas(bitmap);

            nativeView.Draw(canvas);

            canvas.Dispose();

            return bitmap;
        }

        private Android.Views.View ConvertToNative(Context context, IVisualElementRenderer renderer, VisualElement view)
        {
            View nativeView = renderer.View;

            float density = context.Resources.DisplayMetrics.Density;

            double viewWidth = view.Width * density;

            double viewHeight = view.Height * density;

            (_, _, double width, double height) = new Xamarin.Forms.Rectangle(0, 0, viewWidth, viewHeight);

            ViewGroup.LayoutParams layoutParams = new Android.Views.ViewGroup.LayoutParams((int)width, (int)height);

            nativeView.LayoutParameters = layoutParams;

            nativeView.Layout(0, 0, (int)width, (int)height);

            layoutParams.Dispose();

            return nativeView;
        }
    }
}
