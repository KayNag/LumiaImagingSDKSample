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
    public partial class Tutorialsecond : PhoneApplicationPage
    {
        public Tutorialsecond()
        {
            InitializeComponent();
            
        }
        private void BtnCam_Click(object sender, RoutedEventArgs e)
        {
            PerfectCamera.DataContext.Instance.CameraType = PerfectCameraType.Selfie;
            NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));
        }
    }
}