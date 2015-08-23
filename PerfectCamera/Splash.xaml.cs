using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PerfectCamera;
using System.Threading;
using System.IO.IsolatedStorage;

namespace PerfectCamera
{
    public partial class Splash : PhoneApplicationPage
    {
        public Splash()
        {
            InitializeComponent();
            Loaded += SplashPage_Loaded;

        }

     
        void SplashPage_Loaded(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(3000);
            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            if (IsolatedStorageSettings.ApplicationSettings.Contains("IsFirstTimeLaunched") && settings["IsFirstTimeLaunched"] != "true")
            {
                PerfectCamera.DataContext.Instance.CameraType = PerfectCameraType.Selfie;
                NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));
            }
            else
            {
                settings["IsFirstTimeLaunched"] = "false";
                settings.Save();
                PerfectCamera.DataContext.Instance.CameraType = PerfectCameraType.Selfie;
                NavigationService.Navigate(new Uri("/Tutorial.xaml", UriKind.Relative));

            }
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Terminate();
        }



    }
}
