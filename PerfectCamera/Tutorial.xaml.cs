using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading;


namespace PerfectCamera
{
    public partial class Tutorial : PhoneApplicationPage
    {
        public Tutorial()
        {
         
            InitializeComponent();
            GoogleAnalytics.EasyTracker.GetTracker().SendView("Tutorial View");

        }
        private void BtnCam_Click(object sender, RoutedEventArgs e)
        {
            if (PerfectCamera.DataContext.Instance.screenfrom == ScreenFrom.captured| PerfectCamera.DataContext.Instance.screenfrom == ScreenFrom.pick)
            {
                NavigationService.GoBack();
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("Tutorial Done click", "From Captured|pick screen", null, 0);
            }
            else
            {
                NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));
            }
           
           
        }
    }
}