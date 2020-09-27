namespace VisualDebug.Models
{
    public class RenderThickness
    {
        public double Bottom { get; set; }
        public double Right { get; set; }
        public double Top { get; set; }
        public double Left { get; set; }

        public override string ToString()
        {
            return $"{{Left={Left} Top={Top} Right={Right} Bottom={Bottom}}}";
        }
    }
}