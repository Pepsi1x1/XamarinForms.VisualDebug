using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vdm = VisualDebug.Models;

namespace XamarinForms.VisualDebug.Core
{
    public class Sender
    {
        private static readonly System.Net.Http.HttpClientHandler httpClientHandler = new System.Net.Http.HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
        };

        private static readonly System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient(httpClientHandler);

        public async System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendToServer(string packet, string ip = "", string port = "3000")
        {
            if(string.IsNullOrWhiteSpace(ip))
            {
                ip = GetHostLocalIp();
            }

            const string Json = "application/json";

            return await httpClient.PostAsync($"https://{ip}:{port}/treedatas", new System.Net.Http.StringContent(packet, System.Text.Encoding.UTF8, Json));
        }

        public string GetHostLocalIp()
        {
            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    return "10.0.2.2";
                    break;
                default:
                    return "127.0.0.1";
                    break;
            }

        }
    }
}
