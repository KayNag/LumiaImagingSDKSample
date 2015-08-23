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
using Windows.Storage;
using Windows.System;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using Microsoft.Phone.Shell;
using FiftyNine.Ag.OpenXML.Excel;
using FiftyNine.Ag.OpenXML.Common.Storage;
using System.Collections.Generic;
using System.Linq;


namespace PerfectCamera
{
    public partial class test : PhoneApplicationPage
    {

        WriteableBitmap wb;
        //Boolean isTaskLaunched;
        Boolean istglchecked;
        Boolean isBanner;
        PhotoChooserTask PhotoChooserTask;
        private bool isTaskLaunched;
        string txt = "";
        StringBuilder sb = new StringBuilder();
        Boolean isTglChecked;
        Boolean FileChoosed;
        WriteableBitmap wbmp;
        WriteableBitmap xx1;
        int pixelSize = 40;
        BitmapImage bmp;
        ApplicationBarMenuItem mItem = new ApplicationBarMenuItem();
        String path,orginalpath;
        public List<string> colourlist = new List<string> { "ffffffff", "ff000000" };
        public test()
        {
            InitializeComponent();
            isTglChecked = true;
            isBanner = true;
            this.PhotoChooserTask = new PhotoChooserTask();
            this.PhotoChooserTask.Completed += new EventHandler<PhotoResult>(photocoosertask_completed);
            isTaskLaunched = false;
           ApplicationBarMenuItem baannerOnOff = new ApplicationBarMenuItem();
            baannerOnOff.Text = "Excel Banner Off";
            ApplicationBar.MenuItems.Add(baannerOnOff);
           baannerOnOff.Click += new EventHandler(bannerOnOff_Click);
            ApplicationBarMenuItem about = new ApplicationBarMenuItem();
            about.Text = "About";
            ApplicationBar.MenuItems.Add(about);
            about.Click += new EventHandler(About_Click);
            GoogleAnalytics.EasyTracker.GetTracker().SendView("Open File view");
            if (PerfectCamera.DataContext.Instance.Pixels == PixelSize.ten)
            {
                pixelSize = 20;

            }
            else if (PerfectCamera.DataContext.Instance.Pixels == PixelSize.fifteen)
            {
                pixelSize = 30;
            }
            else if (PerfectCamera.DataContext.Instance.Pixels == PixelSize.thirty)
            {
                pixelSize = 60;
            }
        }
      
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Thread.Sleep(5000);
            proceed();

        }
     
        private void proceed()
        {

            if (!isTaskLaunched)
            {
                this.PhotoChooserTask = new PhotoChooserTask();
                this.PhotoChooserTask.Completed += new EventHandler<PhotoResult>(photocoosertask_completed);
                isTaskLaunched = true;
                PhotoChooserTask.Show();
             
            }


        }
        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
          
        }
        private void Tutorial_Click(object sender, EventArgs e)
        {
            PerfectCamera.DataContext.Instance.screenfrom = ScreenFrom.pick;
            NavigationService.Navigate(new Uri("/Tutorial.xaml", UriKind.Relative));
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("Tutorial Button click", "Open File Screen", null, 0);
        }
        private void bannerOnOff_Click(object sender, EventArgs e)
        {
            ApplicationBarMenuItem mi = (ApplicationBarMenuItem)ApplicationBar.MenuItems[1];

            if (mi.Text == "Excel Banner On")
            {
                mi.Text = "Excel Banner Off";
                isBanner = true;
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("Banner off click", "Open File Screen", null, 0);
            }
            else if (mi.Text == "Excel Banner Off")
            {
                mi.Text = "Excel Banner On";
              
                isBanner = false;
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("Banner On click", "Open File Screen", null, 0);
            } 
        }

        public void photocoosertask_completed(object sender, PhotoResult e)
        {
            try
            {
                if (e.TaskResult == TaskResult.OK)
                {
                    path = e.OriginalFileName;
                    orginalpath = path;
                    FileChoosed = true;
                    bmp = new BitmapImage();
                    bmp.SetSource(e.ChosenPhoto);
                    wbmp = new WriteableBitmap(bmp);
                        fn_ExcelColor(wbmp);
                       
                   
                }
                else if (e.TaskResult == TaskResult.None || e.TaskResult == TaskResult.Cancel)
                {
                    FileChoosed = false;
                    PerfectCamera.DataContext.Instance.CameraType = PerfectCameraType.Selfie;
                    NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));

                    this.Dispatcher.BeginInvoke(delegate()
                    {
                    });

                }

                MainImage.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void btnshares_Click(object sender, EventArgs e)

        {

            try
            {
                GoogleAnalytics.EasyTracker.GetTracker().SendEvent("share Button click", "Open File Screen", null, 0);

                using (MediaLibrary mb = new MediaLibrary())
                {
                    if(isBanner)
                    {
                        screencapture();
                        ShowShareMediaTask(path);
                    }
                    else
                    {
                        path = orginalpath;
                        ShowShareMediaTask(path);
                    }
                                
                    
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        void ShowShareMediaTask(string path)
        {
            ShareMediaTask shareMediaTask = new ShareMediaTask();
            shareMediaTask.FilePath = path;
            shareMediaTask.Show();
        }
        protected async void downloadBtn_click(object sender, EventArgs e)
        {
            excelDownload(bmp);
            GoogleAnalytics.EasyTracker.GetTracker().SendEvent("Download on Excel click", "Open File Screen", null, 0);

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
        

        private void btnbacks_Click(object sender, EventArgs e)
        {

            NavigationService.Navigate(new Uri("/CameraPage.xaml", UriKind.Relative));
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

                    bmp.SetSource(img);
                }
                    wbmp = new WriteableBitmap(bmp);

               
                    fn_ExcelColor(wbmp);
               
               
               

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
                int w1;
                int h1;
                if(wmb.PixelHeight >1000 || wmb.PixelWidth > 1000)
                {
                    w1 = wmb.PixelWidth / pixelSize;
                    h1 = wmb.PixelHeight / pixelSize;
                }
                else
                {
                    w1 = wmb.PixelWidth / (pixelSize / 2);
                    h1 = wmb.PixelHeight / (pixelSize / 2);
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
                    int row = a/ w1;
                    int col = a % w1;  
                    for (int k = 0; k < 10; k++)
                    {
                        for (int l = 0; l <10; l++)
                        {
                            int p1 = swb.Pixels[a];
                            byte[] b1 = BitConverter.GetBytes(p1);
                            extension.SetPixel(xx1, (row * 10) + k, (col * 10) + l, b1[3], b1[2], b1[1], b1[0]);        
                        }

                    }

                }
                txtHtml.Visibility = Visibility.Collapsed;
                MainImage.Visibility = Visibility.Visible;
                MainImage.Source = xx1;
                MainImage.Stretch = Stretch.UniformToFill;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        private void excelDownload(BitmapImage wmb)
        {
            try
            {
                int width = wmb.PixelWidth / pixelSize;
                int height = wmb.PixelHeight / pixelSize;
                WriteableBitmap wmba = new WriteableBitmap(wmb);
                MemoryStream ms = new MemoryStream();
                wmba.SaveJpeg(ms, width, height, 0, 100);
                BitmapImage imgbt = new BitmapImage();
                imgbt.SetSource(ms);
                WriteableBitmap swb = new WriteableBitmap(imgbt);
                xx1 = new WriteableBitmap(swb.PixelWidth, swb.PixelHeight);
                int[] srcpic = swb.Pixels;
                string[,] pixelcolours = new string[height, width];
                //      prime variables and loop
                int currpix = 0;
                string hexvalue = "";

                int a = 0;
                for (int row = 0; row < height; row++) //same size as the image
                {
                    for (int col = 0; col < width; col++)
                    {
                        int p1 = swb.Pixels[a];

                        hexvalue = String.Format("{0}", swb.Pixels[a].ToString("X2"));
                        //   hexvalue = String.Format("{3}{2}{1}{0}", pixelBytes[currpix].ToString("X2"), pixelBytes[currpix + 1].ToString("X2"), pixelBytes[currpix + 2].ToString("X2"), pixelBytes[currpix + 3].ToString("X2"));
                        //      populate the array
                        pixelcolours[row, col] = hexvalue;

                        a = a + 1; //increment the per-pixel offset
                    }
                }


                int[,] pixels = new int[2, 2];  //should do a better constructor so it doesnt get re-dimensioned but this works.
                pixels = replaceColours(pixelcolours);
                String[] colourtable = new String[] { };
                colourtable = colourlist.ToArray();
                makespreadsheet(colourtable, pixels);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void makespreadsheet(string[] colourtable, int[,] pixels)   //probably should move to a static.
        {
            var doc = new SpreadsheetDocument();
            doc.ApplicationName = "SilverSpreadsheet";
            doc.Creator = "Excel Art";
            doc.Company = "Microsoft";
            for (int colour = 0; colour < colourtable.GetLength(0); colour++)
            {
                doc.Workbook.WorkbookStyles.AddFill(colourtable[colour], "ffffffff");
                doc.Workbook.WorkbookStyles.AddCellxfs(colour);
            }
            for (int row = 0; row < pixels.GetLength(0); row++) //assumes not jagged array
            {
                for (int col = 0; col < pixels.GetLength(1); col++)
                {
                    // now itterate through each row and column to get the sytle index 
                    doc.Workbook.Sheets[0].Sheet.Rows[row].Cells[col].SetStyle(pixels[row, col]); // predicated on one-to-one match between styles and colours
                }
            }
            doc.Workbook.Sheets[0].Sheet.AddColumnSizeDefinition(0, pixels.GetLength(1) - 1, 3);
            SaveXlsxToIsoStoreAndLaunchInExcel(doc);
        }

        public int[,] replaceColours(string[,] colourpixels)
        {
            int[,] _pixels = new int[colourpixels.GetLength(0), colourpixels.GetLength(1)];

            for (int row = 0; row < colourpixels.GetLength(0); row++) //assumes not jagged array
            {
                for (int col = 0; col < colourpixels.GetLength(1); col++)
                {
                    _pixels[row, col] = AddColourGetInt((string)colourpixels[row, col]);
                }
            }
            return _pixels;
        }



        public int AddColourGetInt(string colour)
        {
            int ret;
            if (colour == colourlist.FirstOrDefault(s => s == colour))
            {
                //already exists
                ret = colourlist.IndexOf(colour);
            }
            else
            {
                ret = colourlist.Count();

                colourlist.Add(colour);
            }

            return ret;
        }

        private async void SaveXlsxToIsoStoreAndLaunchInExcel(SpreadsheetDocument doc)
        {
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStore.FileExists("ExcelArt.xlsx"))
                    isoStore.DeleteFile("ExcelArt.xlsx");

                using (System.IO.FileStream s = isoStore.OpenFile("ExcelArt.xlsx", System.IO.FileMode.Create, System.IO.FileAccess.Write))
                using (IStreamProvider storage = new ZipStreamProvider(s))
                {
                    doc.Save(storage);
                }
            }

            StorageFile s_file = await ApplicationData.Current.LocalFolder.GetFileAsync("ExcelArt.xlsx");
            await Windows.System.Launcher.LaunchFileAsync(s_file);
        }


        protected async void screencapture()
        {
            var bmp = new WriteableBitmap(LayoutRoot, new TranslateTransform());
            var width = (int)bmp.PixelWidth;
            var height = (int)bmp.PixelHeight;
            bmp.Render(LayoutRoot, new TranslateTransform());
            using (var ms = new MemoryStream())
            {
                bmp.SaveJpeg(ms, width, height, 0, 100);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                var lib = new MediaLibrary();
                var dateStr = DateTime.Now.Ticks;
                var picture = lib.SavePicture(string.Format("screenshot" + dateStr + ".jpg"), ms);
                path = picture.GetPath();
              
            }

        }


    }
}