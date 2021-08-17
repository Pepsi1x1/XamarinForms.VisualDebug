using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Core
{
    public static class LayoutExtensions
    {
        public static string ToFullName(this LayoutOptions options)
        {
            return options.Alignment + (options.Expands ? "AndExpand" : "");
        }
    }
}
