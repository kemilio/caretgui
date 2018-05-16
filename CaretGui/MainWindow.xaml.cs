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
using MahApps.Metro.Controls;
using ImageProcessor.Imaging.Formats;
using ImageProcessor;
using System.Drawing.Imaging;

namespace CaretGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private String origFilePath = "";
        private String newFilePath = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void to_PNG()
        {
            try
            {
                byte[] photoBytes = File.ReadAllBytes(origFilePath);
                // Format is automatically detected though can be changed.
                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Do something with the stream.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Save(outStream);

                        }

                        byte[] newImageBytes = outStream.ToArray();

                        System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                        img.Save(outStream, ImageFormat.Png);

                        //might require Admin priviledges (i.e. VS run as admin)
                        Console.WriteLine(getNewFilePath() + ".png");
                        FileStream file = new FileStream(getNewFilePath() + ".png", FileMode.Create, FileAccess.Write);
                        outStream.WriteTo(file);
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_JPEG()
        {
            try
            {
                byte[] photoBytes = File.ReadAllBytes(origFilePath);
                // Format is automatically detected though can be changed.
                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Do something with the stream.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Save(outStream);

                        }

                        byte[] newImageBytes = outStream.ToArray();

                        System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                        img.Save(outStream, ImageFormat.Jpeg);

                        //might require Admin priviledges (i.e. VS run as admin)
                        Console.WriteLine(getNewFilePath() + ".jpeg");
                        FileStream file = new FileStream(getNewFilePath() + ".jpeg", FileMode.Create, FileAccess.Write);
                        outStream.WriteTo(file);
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_BMP()
        {
            try
            {
                byte[] photoBytes = File.ReadAllBytes(origFilePath);
                // Format is automatically detected though can be changed.
                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Do something with the stream.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Save(outStream);

                        }

                        byte[] newImageBytes = outStream.ToArray();

                        System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                        img.Save(outStream, ImageFormat.Bmp);

                        //might require Admin priviledges (i.e. VS run as admin)
                        Console.WriteLine(getNewFilePath() + ".bmp");
                        FileStream file = new FileStream(getNewFilePath() + ".bmp", FileMode.Create, FileAccess.Write);
                        outStream.WriteTo(file);
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void to_TIFF()
        {
            try
            {
                byte[] photoBytes = File.ReadAllBytes(origFilePath);
                // Format is automatically detected though can be changed.
                using (MemoryStream inStream = new MemoryStream(photoBytes))
                {
                    using (MemoryStream outStream = new MemoryStream())
                    {
                        // Do something with the stream.
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            // Load, resize, set the format and quality and save an image.
                            imageFactory.Load(inStream)
                                        .Save(outStream);

                        }

                        byte[] newImageBytes = outStream.ToArray();

                        System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                        img.Save(outStream, ImageFormat.Tiff);

                        //might require Admin priviledges (i.e. VS run as admin)
                        Console.WriteLine(getNewFilePath() + ".tiff");
                        FileStream file = new FileStream(getNewFilePath() + ".tiff", FileMode.Create, FileAccess.Write);
                        outStream.WriteTo(file);
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened! Please try again. \n" + ex.Message);
            }
        }

        private void Crop_Execute()
        {
            int wResult = 0;
            int hResult = 0;
            Int32.TryParse(Width_Input.Text, out wResult);
            Int32.TryParse(Height_Input.Text, out hResult);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(0, 0, wResult, hResult);

            byte[] photoBytes = File.ReadAllBytes(origFilePath);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        try
                        {
                            imageFactory.Load(inStream)
                                    .Crop(rectangle)
                                    .Format(format)
                                    .Save(outStream);
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Something happened! Did you reasonable numbers in the height/width box? \n" + ex.Message);
                            return;
                        }

                    }
                    // Do something with the stream.


                    byte[] newImageBytes = outStream.ToArray();

                    System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                    img.Save(outStream, ImageFormat.Jpeg);

                    //might require Admin priviledges (i.e. VS run as admin)
                    Console.WriteLine(getNewFilePath() + ".jpeg");
                    FileStream file = new FileStream(getNewFilePath() + ".jpeg", FileMode.Create, FileAccess.Write);
                    outStream.WriteTo(file);
                    file.Close();
                    origFilePath = getNewFilePath() + ".jpeg";
                }
            }
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog(); 

            if (!string.IsNullOrWhiteSpace(dlg.FileName))
            {
                origFilePath = dlg.FileName;
                Image_File_Path.Text = origFilePath;
            }
        }

        private void Browse_New_Location_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            if (!string.IsNullOrWhiteSpace(dlg.FileName))
            {
                newFilePath = dlg.FileName;
                Image_Location_Name.Text = newFilePath;
            }
        }

        //returns new file path without any extension (needs extension to be generated)
        private String getNewFilePath()
        {
            String name = "";
            String path = "";

            //set new name or default to original_Edit
            if (Image_New_Name.Text == "")
            {
                String[] parsed = Image_File_Path.Text.Split('\\');
                name = parsed[parsed.Length - 1];
                name = name.Substring(0, name.IndexOf(".")); //trims file extension off of name
                name += "_Edit";
            }
            else
                name = Image_New_Name.Text;


            //set new path or default to original path
            if (Image_Location_Name.Text == "")
            {
                String[] parsed = Image_File_Path.Text.Split('\\');

                for (int i = 0; i < parsed.Length - 1; i++)
                {
                    if (i != 0)
                        path += @"\";
                    path += parsed[i];
                }
            }
            else
                path = Image_Location_Name.Text;


            String result = path + @"\" + name;
            Console.WriteLine("Debug path: " + path);
            Console.WriteLine("Debug name: " + name);
            Console.WriteLine("Debug: " + result);
            return result;

        }

        private void Brightness_Execute()
        {
            int brightness = 0;
            Int32.TryParse(Brightness_Input.Text, out brightness);

            byte[] photoBytes = File.ReadAllBytes(origFilePath);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Brightness(brightness)
                                    .Format(format)                             
                                    .Save(outStream);

                    }
                    // Do something with the stream.


                    byte[] newImageBytes = outStream.ToArray();

                    System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                    img.Save(outStream, ImageFormat.Jpeg);

                    //might require Admin priviledges (i.e. VS run as admin)
                    Console.WriteLine(getNewFilePath() + ".jpeg");
                    FileStream file = new FileStream(getNewFilePath() + ".jpeg", FileMode.Create, FileAccess.Write);
                    outStream.WriteTo(file);
                    file.Close();
                    origFilePath = getNewFilePath() + ".jpeg";
                }
            }
        }

        private void Hue_Execute()
        {
            int hue = 0;
            Int32.TryParse(Hue_Input.Text, out hue);

            byte[] photoBytes = File.ReadAllBytes(origFilePath);
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Hue(hue)
                                    .Format(format)
                                    .Save(outStream);

                    }
                    // Do something with the stream.


                    byte[] newImageBytes = outStream.ToArray();

                    System.Drawing.Image img = System.Drawing.Image.FromStream(outStream);

                    img.Save(outStream, ImageFormat.Jpeg);

                    //might require Admin priviledges (i.e. VS run as admin)
                    Console.WriteLine(getNewFilePath() + ".jpeg");
                    FileStream file = new FileStream(getNewFilePath() + ".jpeg", FileMode.Create, FileAccess.Write);
                    outStream.WriteTo(file);
                    file.Close();
                    origFilePath = getNewFilePath() + ".jpeg";
                }
            }
        }

        private void Master_Click(object sender, RoutedEventArgs e)
        {
            if (Hue_Input.Text != null)
            {
                Hue_Execute();
            }

            if (Brightness_Input.Text != null)
            {
                Brightness_Execute();
            }

            if (Height_Input != null && Width_Input != null)
            {
                Crop_Execute();
            }

            switch(ComboBox.Text)
            {
                case ".PNG":
                    to_PNG();
                    break;
                case ".JPEG":
                    to_JPEG();
                    break;
                case ".BMP":
                    to_BMP();
                    break;
                case ".TIFF":
                    to_TIFF();
                    break;
                default:
                    MessageBox.Show("Please select a type for the file to save as!");
                    break;
            }

            MessageBox.Show("Photo has been saved!");
        }
    }
}
