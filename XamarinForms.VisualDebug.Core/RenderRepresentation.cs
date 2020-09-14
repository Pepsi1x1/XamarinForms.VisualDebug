using System;
using System.Collections.Generic;

namespace XamarinForms.VisualDebug.Core
{
    public class RenderRepresentation
    {
        public string VisualTypeName { get; set; }

        public Guid ElementId { get; set; }

        public string ParentVisualTypeName { get; set; }

        public Guid ParentId { get; set; }

        public double WidthRequest { get; set; }

        public double HeightRequest { get; set; }

        public RenderBounds Bounds { get; set; }

        public List<RenderRepresentation> Children { get; set; }
        public byte[] ViewPng { get; internal set; }

        public override string ToString()
        {
            return $"{this.VisualTypeName} Parent {this.ParentVisualTypeName} - WidthR {this.WidthRequest} HeightR {this.HeightRequest} Bounds {this.Bounds}{Environment.NewLine}";
        }
    }
}