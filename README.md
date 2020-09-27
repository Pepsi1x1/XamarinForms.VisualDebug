# XamarinForms.VisualDebugger

A library and client app to view the visual heirarchy of your Xamarin app pages as an interactive tree diagram

![Preview](https://github.com/Pepsi1x1/XamarinForms.VisualDebug/raw/master/final-5f6f58c13e5b3e0061bb6cf9-6.gif "Preview")

# Getting Started

Install the XamarinForms.VisualDebug Nuget packages into your Core AND platform specific projects

Install-Package XamarinForms.VisualDebug -Version 0.0.1
dotnet add package XamarinForms.VisualDebug --version 0.0.1

## Core project

For the simplest use case in App.xaml.cs include the following

```c#
       public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            //Add this line
            Current.PageAppearing += Current_PageAppearing;
        }

        private static XamarinForms.VisualDebug.Core.Sender Sender = new XamarinForms.VisualDebug.Core.Sender();

        private void Current_PageAppearing(object sender, Page e)
        {
            var rep = XamarinForms.VisualDebug.Core.TreeRenderer.RenderVisualHeirarchyToJson(e);

            System.Threading.Tasks.Task.Run(async () =>
            {
                var response = await Sender.SendToServer(rep).ConfigureAwait(false);
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
```

## iOS

Add the following line to FinishedLaunching in AppDelegate.cs somehwher after the Xamarin.Forms.Forms.Init call

```c#
            XamarinForms.VisualDebug.iOS.VisualDebug.Init();
```       
            
## Android

Add the following line to FinishedLaunching in AppDelegate.cs somehwher after the Xamarin.Forms.Forms.Init call

```c#
            XamarinForms.VisualDebug.Droid.VisualDebug.Init();
```
            
## Electron app

Run the client before starting the simluator/emulator for your Xamarin app

```sh
$ cd XamarinForms.VisualDebug.App
# Install dependencies
$ npm install
# Run the app
$ npm start     
```
