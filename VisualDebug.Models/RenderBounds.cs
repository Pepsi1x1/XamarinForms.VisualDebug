namespace VisualDebug.Models
{
    public class RenderBounds
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public override string ToString()
        {
            return $"{{X={X} Y={Y} Width={Width} Height={Height}}}";
        }
    }
}