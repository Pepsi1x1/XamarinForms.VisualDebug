using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Core
{
    public interface INativeViewRenderer
    {
        byte[] Render(VisualElement rootElement);
    }
}