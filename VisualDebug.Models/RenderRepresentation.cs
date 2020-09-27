using System;
using System.Collections.Generic;

namespace VisualDebug.Models
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

        public RenderThickness Margin { get; set; }

        public RenderThickness Padding { get; set; }

        [Newtonsoft.Json.JsonProperty(DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Ignore, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore, PropertyName = "children", Required = Newtonsoft.Json.Required.AllowNull)]
        [System.Runtime.Serialization.DataMember(EmitDefaultValue = false, IsRequired = false, Name = "children")]
        public List<RenderRepresentation> Children { get; set; }

        public byte[] ViewPng { get; set; }

        public override string ToString()
        {
            return $"{VisualTypeName} Parent {ParentVisualTypeName} - WidthR {WidthRequest} HeightR {HeightRequest} Bounds {Bounds}{Environment.NewLine}";
        }
    }
}