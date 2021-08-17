using System;
using VisualDebug.Models;
using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Core
{
    internal static class RenderRepresentationMapper
    {
        private const string PADDING_PROPERTY_NAME = "Padding";

        internal static RenderRepresentation ToRenderRepresentation(this VisualElement element)
        {
            if(element is null)
            {
                return null;
            }

            var rep = new RenderRepresentation()
            {
                ElementId = element.Id,
                VisualTypeName = element.GetType().Name,
                ParentVisualTypeName = element.Parent?.GetType().Name,
                ParentId = element.Parent?.Id ?? Guid.Empty,
                WidthRequest = element.WidthRequest,
                HeightRequest = element.HeightRequest,
            };

            rep.Bounds = element.Bounds.ToRenderBounds();

            if (element is View view)
            {
                rep.HorizontalOptions = view.HorizontalOptions.ToFullName();

                rep.VerticalOptions = view.VerticalOptions.ToFullName();

                rep.Margin = view.Margin.ToRenderThickness();
            }

            var paddingProp = element.GetType().GetProperty(PADDING_PROPERTY_NAME);
            if (!(paddingProp is null))
            {
                var padding = (Thickness)paddingProp.GetValue(element);

                rep.Padding = padding.ToRenderThickness();
            }

            return rep;
        }

        internal static RenderThickness ToRenderThickness(this Thickness thickness)
        {
            return new RenderThickness
            {
                Left = thickness.Left,
                Top = thickness.Top,
                Right = thickness.Right,
                Bottom = thickness.Bottom
            };
        }

        internal static RenderBounds ToRenderBounds(this Rectangle rect)
        {
            return new RenderBounds()
            {
                X = rect.X,
                Y = rect.Y,
                Height = rect.Height,
                Width = rect.Width
            };
        }
    }
}
