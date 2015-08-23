using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Text;
using Microsoft.Phone.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework.Media;
using System.Linq;
using Windows.Storage;
using Windows.System;

namespace PerfectCamera
{
    public partial class Preview : PhoneApplicationPage
    {
        WriteableBitmap wb;
        //Boolean isTaskLaunched;
        Boolean istglchecked;
        PhotoChooserTask PhotoChooserTask;
        private bool isTaskLaunched;
        string txt = "";
        StringBuilder sb = new StringBuilder();
        Boolean isTglChecked;
        Boolean FileChoosed;
        WriteableBitmap wbmp;
        WriteableBitmap xx1;
        String imgname;
        String imgpath;
        public static StorageFolder CameraRoll { get; }

        public Preview()
        {
            InitializeComponent();
            isTglChecked = true;
           
            isTaskLaunched = false;
            LoadImg();

        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Thread.Sleep(5000);
           

        }
      

        private void merge_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform trans = new TranslateTransform();
            wb = new WriteableBitmap((BitmapSource)myimage.Source);
            tb.Text = "%$*>?**&&|10*>?**&&|20*>?**&&|30*>?**&&|40*>?**&&|50*>?**&&|60*>?**&&|70*>?**&&|80^^^^^^ASHSAHHFFASKLALLAOWIIEIIEIIE|";
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Foreground = new SolidColorBrush(Colors.White);
            tb.FontSize = 20;
            tb.FontWeight = FontWeights.Bold;
            wb.Render(tb, null);
            wb.Invalidate();
            finimg.Source = wb;

        }
        void cameraCaptureTask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                ShowShareMediaTask(e.OriginalFileName);
            }
        }

        void ShowShareMediaTask(string path)
        {
            ShareMediaTask shareMediaTask = new ShareMediaTask();
            shareMediaTask.FilePath = path;
            shareMediaTask.Show();
        }




        private void share_click(object sender, RoutedEventArgs e)
        {
            //String name= imgname ;
            //String path = CameraRoll.Path + name;
            PhotoChooserTask cameraCaptureTask = new PhotoChooserTask();
            cameraCaptureTask.Completed += cameraCaptureTask_Completed;
            cameraCaptureTask.Show();
           
          
            

        }
    
        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            btnWord.Visibility = Visibility.Visible;
            btnExcel.Visibility = Visibility.Collapsed;
            BitmapImage bmp = new BitmapImage();
            using (MediaLibrary mb = new MediaLibrary())
            {
                int cnt = mb.Pictures.Count - 1;

                Stream img = mb.Pictures[cnt].GetImage();

                bmp.SetSource(img);
            }
            wbmp = new WriteableBitmap(bmp);
            fn_ExcelColor(wbmp);
        }
        private void btnWord_Click(object sender, RoutedEventArgs e)
        {
            btnWord.Visibility = Visibility.Collapsed;
            btnExcel.Visibility = Visibility.Visible;
            
            BitmapImage bmp = new BitmapImage();
            using (MediaLibrary mb = new MediaLibrary())
            {
                int cnt = mb.Pictures.Count - 1;

                Stream img = mb.Pictures[cnt].GetImage();

                bmp.SetSource(img);
            }
            wbmp = new WriteableBitmap(bmp);

          
                fn_camera1(wbmp);


        }
        private async void download_click(object sender, RoutedEventArgs e)
        {
            try
            {

                // for launch 
                /*  StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                  StorageFile pdffile = await local.GetFileAsync("test.doc");

                  // Launch the pdf file
                  await Windows.System.Launcher.LaunchFileAsync(pdffile);*/
                // StorageFolder folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile file = await local.GetFileAsync("check.docx");
                await Launcher.LaunchFileAsync(file);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void fn_filesave(WriteableBitmap sourcewb)
        {
            try
            {
                WriteableBitmap savewb = new WriteableBitmap(sourcewb);
                using (var isolatedstorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (isolatedstorage.FileExists("test.jpg"))
                    {
                        isolatedstorage.DeleteFile("test.jpg");

                    }
                }
                using(var medialibry=new MediaLibrary())
                {
                    using(MemoryStream ms=new MemoryStream())
                    {
                        sourcewb.SaveJpeg(ms, sourcewb.PixelWidth, sourcewb.PixelHeight, 0, 100);
                        ms.Seek(0, SeekOrigin.Begin);
                        var picture = medialibry.SavePicture("test.jpg", ms);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnbcak_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));
        }
        private void fn_camera1(WriteableBitmap wmb)
        {
            try
            {
                int w1;
                int h1;
                if (wmb.PixelWidth > 1000 || wmb.PixelHeight > 1000)
                {
                    w1 = wmb.PixelWidth / 10;
                    h1 = wmb.PixelHeight / 20;
                }
                else
                {
                    w1 = wmb.PixelWidth / 5;
                    h1 = wmb.PixelHeight / 5;
                }

                MemoryStream ms = new MemoryStream();
                wmb.SaveJpeg(ms, w1, h1, 0, 100);
                BitmapImage imgbt = new BitmapImage();
                imgbt.SetSource(ms);
                WriteableBitmap swb = new WriteableBitmap(imgbt);
                int[] srcpic = swb.Pixels;
                txt = "";
                string characters = " .,:;i1tfLCG08@";

                for (int a = 0; a < srcpic.Length; a++)
                {
                    int pixel = swb.Pixels[a];

                    int row = a / w1;
                    int col = a % w1;

                    byte[] bytes = BitConverter.GetBytes(pixel);

                    Color clr = Color.FromArgb(255, bytes[0], bytes[1], bytes[2]);
                    double cred = Math.Floor((bytes[0] - 128) * 1.3681) + 128;
                    double cgre = Math.Floor((bytes[1] - 128) * 1.3681) + 128;
                    double cblu = Math.Floor((bytes[2] - 128) * 1.3681) + 128;
                    double calp = bytes[3];

                    double bright = (0.299 * cred + 0.587 * cgre + 0.114 * cblu) / 255;

                    double ret = (14) - Math.Round(bright * 14);
                    if (ret > 13)
                    {
                        ret = 13;
                    }
                    if (ret < 0)
                    {
                        ret = 0;
                    }
                    txt += characters.Substring(Convert.ToInt32(ret), 1);
                    if (a > 0 && a % w1 == 0)
                    {
                        txt += System.Environment.NewLine;
                    }
                }

                finimg.Visibility = Visibility.Collapsed;
                tb.Visibility = Visibility.Visible;
                wb = new WriteableBitmap((BitmapSource)myimage.Source);
                tb.Text = txt;
                tb.FontSize = 2.8;
                tb.Foreground = new SolidColorBrush(Colors.Black);
                tb.FontFamily = new System.Windows.Media.FontFamily("Lucida Console");
                tb.FontWeight = FontWeights.ExtraBold;
                double f = (((640 / tb.ActualHeight) * 3) + ((480 / tb.ActualWidth) * 3)) / 2;
                tb.FontSize = f;
                /*wb.Render(tb, null);
                finimg.Source = wb;
                wb.Invalidate();
               */
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void LoadImg()
        {
            try
            {
                BitmapImage bmp = new BitmapImage();
                using (MediaLibrary mb = new MediaLibrary())
                {
                    int cnt = mb.Pictures.Count - 1;

                    Stream img = mb.Pictures[cnt].GetImage();
                     imgname = mb.Pictures[cnt].Name;
                   
                    bmp.SetSource(img);
                }
                    wbmp = new WriteableBitmap(bmp);

                if (isTglChecked == true)
                {
                    fn_ExcelColor(wbmp);
                }
                else
                {
                    fn_camera1(wbmp);

                }
               
               

            }
            catch (Exception)
            {

                throw;
            }
        }
       

        private void fn_ExcelColor(WriteableBitmap wmb)
        {
            try
            {

                Color clr;
                int w1;
                int h1;
                if (wmb.PixelWidth > 1000 || wmb.PixelHeight > 1000)
                {
                    w1 = wmb.PixelWidth / 30;
                    h1 = wmb.PixelHeight / 30;
                }
                else
                {
                    w1 = wmb.PixelWidth / 5;
                    h1 = wmb.PixelHeight / 5;
                }

                MemoryStream ms = new MemoryStream();
                wmb.SaveJpeg(ms, w1, h1, 0, 100);
                BitmapImage imgbt = new BitmapImage();
                imgbt.SetSource(ms);
                WriteableBitmap swb = new WriteableBitmap(imgbt);

                xx1 = new WriteableBitmap(swb.PixelWidth * 10, swb.PixelHeight * 10);

                int[] srcpic = swb.Pixels;
                txt = "";


                for (int a = 0; a < srcpic.Length; a++)
                {
                    int row = a / w1;
                    int col = a % w1;

                    
                    for (int k = 0; k < 10; k++)
                    {
                        for (int l = 0; l < 10; l++)
                        {
                            int p1 = swb.Pixels[a];
                            byte[] b1 = BitConverter.GetBytes(p1);
                            extension.SetPixel(xx1, (row * 10) + k, (col * 10) + l, b1[3], b1[2], b1[1], b1[0]);
                        }
                    }

                }

                //wb = new WriteableBitmap((BitmapSource)myimage.Source);

                tb.Visibility = Visibility.Collapsed;
                finimg.Visibility = Visibility.Visible;
                finimg.Source = xx1;
                finimg.Stretch = Stretch.UniformToFill;

                //MainImage.Width = 600;
                //MainImage.Height = 520;

                /*wb.Render(myimage, null);
                finimg.Source = wb;
                wb.Invalidate();
                */

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}