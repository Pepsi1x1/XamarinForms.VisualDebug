using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinForms.VisualDebug.Core
{
    public static class Utility
    {
        private static Core.Sender Sender = new Core.Sender();

        public static void RenderAndSend(VisualElement e, string ip, int port)
        {
            if (e is NavigationPage)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                var rep = XamarinForms.VisualDebug.Core.TreeRenderer.RenderVisualHeirarchyToJson(e);

                System.Threading.Tasks.Task.Run(async () =>
                {
                    await Sender.SendToServer(rep, ip, port.ToString()).ConfigureAwait(false);
                });
            });
        }

        public static void RenderAndSend(VisualElement e, string ip)
        {
            if (e is NavigationPage)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                var rep = XamarinForms.VisualDebug.Core.TreeRenderer.RenderVisualHeirarchyToJson(e);

                System.Threading.Tasks.Task.Run(async () =>
                {
                    await Sender.SendToServer(rep, ip).ConfigureAwait(false);
                });
            });
        }

        public static void RenderAndSend(VisualElement e)
        {
            if (e is NavigationPage)
                return;

            Device.BeginInvokeOnMainThread(() =>
            {
                var rep = XamarinForms.VisualDebug.Core.TreeRenderer.RenderVisualHeirarchyToJson(e);

                System.Threading.Tasks.Task.Run(async () =>
                {
                    await Sender.SendToServer(rep).ConfigureAwait(false);
                });
            });
        }
    }
}
