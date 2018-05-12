using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaretGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapImage currentImage;
        private OpenFileDialog openObject;
        private SaveFileDialog saveObject;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private SaveFileDialog pre_Conversion(String type, object sender, RoutedEventArgs e)
        {
            Nullable<bool> result = false;
            saveObject = new SaveFileDialog();
            if (currentImage != null)
            {
                String filename =
                    openObject.SafeFileName.Substring(0, openObject.SafeFileName.IndexOf('.'));
                saveObject.FileName = filename + "New";
                saveObject.DefaultExt = type;
                saveObject.Filter = type + " image (" + type + ")|*" + type;

                result = saveObject.ShowDialog();
            }
            if (result == true) return saveObject;
            else return null;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            openObject = new OpenFileDialog();

            openObject.InitialDirectory = "Pictures";
            openObject.Filter = "Image Files(*.PNG; *.JPEG; *.BMP; *.TIFF; *.WMP; *.GIF)| *.PNG; *.JPEG; *.BMP; *.TIFF; *.WMP; *.GIF | All files(*.*) | *.*";
            openObject.FilterIndex = 2;
            openObject.RestoreDirectory = true;

            if (openObject.ShowDialog() == true)
            {
                try
                {
                    currentImage = new BitmapImage(new Uri(openObject.FileName));
                    ImageViewer1.Source = new BitmapImage(new Uri(openObject.FileName, UriKind.Absolute));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
                }
            }
        }

        private void to_PNG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".png", sender, e);
                if (saveObject != null)
                {
                    PngBitmapEncoder pngencoder = new PngBitmapEncoder();
                    pngencoder.Frames.Add(BitmapFrame.Create(currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        pngencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_JPEG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".jpeg", sender, e);
                if (saveObject != null)
                {
                    JpegBitmapEncoder jpegencoder = new JpegBitmapEncoder();
                    jpegencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        jpegencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_BMP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".bmp", sender, e);
                if (saveObject != null)
                {
                    BmpBitmapEncoder bmpencoder = new BmpBitmapEncoder();
                    bmpencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        bmpencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_TIFF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".tiff", sender, e);
                if (saveObject != null)
                {
                    TiffBitmapEncoder tiffencoder = new TiffBitmapEncoder();
                    tiffencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        tiffencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_WMP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".wmp", sender, e);
                if (saveObject != null)
                {
                    WmpBitmapEncoder wmpencoder = new WmpBitmapEncoder();
                    wmpencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        wmpencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_GIF_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveObject = pre_Conversion(".gif", sender, e);
                if (saveObject != null)
                {
                    GifBitmapEncoder gifencoder = new GifBitmapEncoder();
                    gifencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                    using (var filestream = new FileStream(saveObject.FileName, FileMode.Create))
                        gifencoder.Save(filestream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void Crop_Click(object sender, RoutedEventArgs e)
        {
            Image croppedImage = new Image();
            croppedImage.Width = 200;
            croppedImage.Margin = new Thickness(5);

            // Create a CroppedBitmap based off of a xaml defined resource.
            CroppedBitmap cb = new CroppedBitmap(
               currentImage, 
               new Int32Rect(0, 0, 105, 50));       //select region rect
            croppedImage.Source = cb;


            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(cb));
            encoder.Save(memoryStream);

            memoryStream.Position = 0;
            bImg.BeginInit();
            bImg.StreamSource = memoryStream;
            bImg.EndInit();

            memoryStream.Close();

            currentImage = bImg;
        }
    }
}
