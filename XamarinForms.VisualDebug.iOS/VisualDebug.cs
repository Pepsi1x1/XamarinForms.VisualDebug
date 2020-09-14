namespace XamarinForms.VisualDebug.iOS
{
    public class VisualDebug
    {
        public static bool IsInitialised { get; private set; } = false;

        public static void Init()
        {
            if (!IsInitialised)
            {
                IsInitialised = true;
            }
        }
    }
}