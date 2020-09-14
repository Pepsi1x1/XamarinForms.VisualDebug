namespace XamarinForms.VisualDebug.Droid
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