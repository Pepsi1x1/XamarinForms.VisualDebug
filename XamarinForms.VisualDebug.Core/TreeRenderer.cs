using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using VisualDebug.Models;
using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Core
{
    public class TreeRenderer
    {
        public static void DebugWriteRenderTree(VisualElement renderedCard)
        {
            var renderTreeJson = JsonConvert.SerializeObject(VisualHeirarchy(renderedCard));
            Debug.WriteLine(renderTreeJson);
        }

        public static string VisualHeirarchyString(VisualElement rootElement)
        {
            string rets = RenderElementInfoString(rootElement);

            foreach (VisualElement childElement in rootElement.LogicalChildren)
            {
                rets += VisualHeirarchyString(childElement);
            }

            return rets;
        }

        //public static string VisualHeirarchyJson(VisualElement rootElement)
        //{
        //    string rets = RenderElementInfoJson(rootElement);

        //    foreach (VisualElement childElement in rootElement.LogicalChildren)
        //    {
        //        rets += VisualHeirarchyJson(childElement);
        //    }

        //    return rets;
        //}

        public static RenderRepresentation VisualHeirarchy(VisualElement rootElement)
        {
            RenderRepresentation rep = ToRenderRepresentation(rootElement);

            INativeViewRenderer nativeViewRenderer = DependencyService.Get<INativeViewRenderer>();

            rep.ViewPng = nativeViewRenderer.Render(rootElement);

            foreach (VisualElement childElement in rootElement.LogicalChildren)
            {
                rep.Children = AddChildren(childElement, rep);
            }

            return rep;
        }

        public static List<RenderRepresentation> AddChildren(VisualElement rootElement, RenderRepresentation rep)
        {
            var list = new List<RenderRepresentation>();

            foreach (VisualElement childElement in rootElement.LogicalChildren)
            {
                var childRep = ToRenderRepresentation(childElement);

                INativeViewRenderer nativeViewRenderer = DependencyService.Get<INativeViewRenderer>();

                rep.ViewPng = nativeViewRenderer.Render(childElement);

                list.Add(childRep);

                childRep.Children = AddChildren(childElement, childRep);
            }

            return list;
        }

        private static string RenderElementInfoString(VisualElement element)
        {
            RenderRepresentation rep = ToRenderRepresentation(element);

            return rep.ToString();
        }

        private static string RenderElementInfoJson(VisualElement element)
        {
            RenderRepresentation rep = VisualHeirarchy(element);

            return JsonConvert.SerializeObject((object)rep, Newtonsoft.Json.Formatting.Indented);
        }

        private static RenderRepresentation ToRenderRepresentation(VisualElement element)
        {
            return new RenderRepresentation()
            {
                ElementId = element.Id,
                VisualTypeName = element.GetType().Name,
                ParentVisualTypeName = element.Parent?.GetType().Name,
                ParentId = element.Parent?.Id ?? Guid.Empty,
                WidthRequest = element.WidthRequest,
                HeightRequest = element.HeightRequest,
                Bounds = new RenderBounds()
                {
                    X = element.Bounds.X,
                    Y = element.Bounds.Y,
                    Height = element.Bounds.Height,
                    Width = element.Bounds.Width
                }
            };
        }

        
    }
}
