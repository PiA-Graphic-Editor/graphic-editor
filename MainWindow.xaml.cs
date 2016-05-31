using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfUI
{
    public partial class MainWindow : Window
    {
        private static AsmProxy asmProxy = new AsmProxy();
        private FilterSettings fs = new FilterSettings();

        private Image inputImage;
        private Image outputImage;

        private string destinationPath;

        enum Categories
        {
            BlacknWhite = 0,
            BlurnSharpening,
            ContrastnBrightness,
            Sepia
        }

        public MainWindow()
        {
            InitializeComponent();
            pgFunction.SelectedObject = fs;
        }

        private int[] getImageInfo(Image image)
        {
            var info = new int[]
            {
                image.Width,
                image.Height,
                image.ByteLength
            };

            return info;
        }

        private void createOutputImage(byte[] bytes)
        {
            outputImage = new Image(bytes, inputImage.ByteLength, inputImage.Width, inputImage.Height, inputImage.DpiX,
                inputImage.DpiY, inputImage.Format);
            showPreview(outputImage.SourceImage);
        }

        private void doBlackAndWhite(ref byte[] outputBytes, int[] imageInfo, int[] properties)
        {
            asmProxy.executeAsmBlackAndWhite(inputImage.getBytes(), outputBytes, imageInfo, properties);
        }

        private void doBlurAndSharpening(ref byte[] outputBytes, int[] imageInfo, int[] properties)
        {
            asmProxy.executeAsmBlurAndSharpening(inputImage.getBytes(), outputBytes, imageInfo, properties);
        }

        private void doContrastAndBrightness(ref byte[] outputBytes, int[] imageInfo, int[] properties)
        {
            asmProxy.executeAsmContrastAndBrightness(inputImage.getBytes(), outputBytes, imageInfo, properties);
        }

        private void doSepia(ref byte[] outputBytes, int[] imageInfo, int[] properties)
        {
            asmProxy.executeAsmSepia(inputImage.getBytes(), outputBytes, imageInfo, properties);
        }

        private void pickSource_Click(object sender, RoutedEventArgs e)
        {
            string path;
            pickFilePath(out path, true);
            sourcePath.Text = path;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache | BitmapCreateOptions.PreservePixelFormat;
            bitmap.EndInit();
            imageSource.Source = bitmap;
            inputImage = new Image(bitmap);

            if (!String.IsNullOrEmpty(path))
            {
                destinationPath = path;
            }
        }

        private bool pickFilePath(out string filePath, bool ensurePathExists, bool pickFolder = false)
        {
            bool picked = false;
            filePath = null;
            
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = pickFolder;
            dialog.EnsurePathExists = ensurePathExists;
            dialog.Filters.Add(new CommonFileDialogFilter("Bitmaps", "*.bmp"));
            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                picked = true;
                filePath = dialog.FileName;
            }
            
            return picked;
        }

        private void btnTransform_Click(object sender, RoutedEventArgs e)
        {
            if(pgFunction.SelectedProperty == null)
            {
                MessageBox.Show("Choose type of transformation!");
                return;
            }
            if (inputImage == null)
            {
                MessageBox.Show("Choose input image first.", "No image", MessageBoxButton.OK);
                return;
            }

            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var propertiesObject = (pgFunction.SelectedObject as FilterSettings);
            int[] properties;

            switch (pgFunction.SelectedProperty.Category)
            {
                case CategoriesNames.BlackAndWhite:
                    properties = new int[] { };
                    doBlackAndWhite(ref outputBytes, imageInfo, properties);
                    break;

                case CategoriesNames.BlurAndSharpening:
                    properties = new int[] { };
                    doBlurAndSharpening(ref outputBytes, imageInfo, properties);
                    break;

                case CategoriesNames.ContrastAndBrightness:
                    properties = new int[] { (int)(propertiesObject.Contrast), (int)(propertiesObject.Brightness) };
                    doContrastAndBrightness(ref outputBytes, imageInfo, properties);
                    break;

                case CategoriesNames.Sepia:
                    properties = new int[] { (int)(propertiesObject.Sepia) };
                    doSepia(ref outputBytes, imageInfo, properties);
                    break;

                default:
                    MessageBox.Show("Choose type of transformation!");
                    break;
            }

            createOutputImage(outputBytes);
        }

        private void showPreview(BitmapSource bmpSrc)
        {
            OutputImage oi = new OutputImage();
            oi.setOutputImage(bmpSrc);
            oi.setDestinationPath(destinationPath);
            oi.Show();
        }
    }
}
