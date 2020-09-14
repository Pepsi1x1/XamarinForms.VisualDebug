namespace XamarinForms.VisualDebug.Core
{
    public class RenderBounds
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public override string ToString()
        {
            return $"{{X={this.X} Y={this.Y} Width={this.Width} Height={this.Height}}}";
        }
    }
}