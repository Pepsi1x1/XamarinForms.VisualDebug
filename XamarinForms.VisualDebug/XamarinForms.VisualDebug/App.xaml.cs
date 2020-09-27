using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using vd = XamarinForms.VisualDebug;

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

        private static vd.Core.Sender Sender = new Core.Sender();

        private void Current_PageAppearing(object sender, Page e)
        {
            var rep = XamarinForms.VisualDebug.Core.TreeRenderer.RenderVisualHeirarchyToJson(e);

            System.Threading.Tasks.Task.Run(async () =>
            {
                string ip = string.Empty;
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                {
                    ip = XamarinForms.VisualDebug.Constants.IPConstant.LocalIP;
                }

                var response = await Sender.SendToServer(rep, ip).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success!");
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                }
            });
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
