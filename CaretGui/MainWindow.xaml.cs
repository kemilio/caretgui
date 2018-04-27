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
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private PngBitmapEncoder pngencoder;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "Pictures";
            openFileDialog.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    currentImage = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage == null) return;
            String filename = 
                openFileDialog.SafeFileName.Substring(0, openFileDialog.SafeFileName.IndexOf('.'));
            
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = filename + "New";
            saveFileDialog.DefaultExt = ".png";
            saveFileDialog.Filter = "png image (.png)|*.png";

            Nullable<bool> result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                pngencoder = new PngBitmapEncoder();
                pngencoder.Frames.Add(BitmapFrame.Create((BitmapImage)currentImage));
                using (var filestream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                    pngencoder.Save(filestream);
            }
        }
    }
}
