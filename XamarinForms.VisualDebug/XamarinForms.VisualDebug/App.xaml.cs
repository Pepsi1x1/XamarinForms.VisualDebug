using System;
using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            Current.PageAppearing += Current_PageAppearing;
        }

        private void Current_PageAppearing(object sender, Page e)
        {
            string ip = string.Empty;
            if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
            {
                ip = XamarinForms.VisualDebug.Constants.IPConstant.LocalIP;
            }

            XamarinForms.VisualDebug.Core.Utility.RenderAndSend(e, ip);
        }

        private void MainPage_LayoutChanged(object sender, EventArgs e)
        {
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
