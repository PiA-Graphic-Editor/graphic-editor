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

        //Dżony załatw ich
        const string CatContrastAndBrightness = "Contrast and brightness";
        const string CatSepia = "Sepia";
        const string CatBlackAndWhite = "Black and white";
        const string CatBlurAndSharpening = "Blur and sharpening";

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
            printOutputImage(outputImage.SourceImage); //instead of: imageDestination.Source = outputImage.SourceImage;
        }

        private void doBlackAndWhite()
        {
            if (inputImage == null)
            {
                MessageBox.Show("Choose input image first.", "No image", MessageBoxButton.OK);
                return;
            }
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var properties = new int[] { };
            asmProxy.executeAsmBlackAndWhite(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void doBlurAndSharpening()
        {
            if (inputImage == null)
            {
                MessageBox.Show("Choose input image first.", "No image", MessageBoxButton.OK);
                return;
            }
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var propertiesObject = (pgFunction.SelectedObject as FilterSettings);
            var properties = new int[] { };
            asmProxy.executeAsmBlurAndSharpening(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void doContrastAndBrightness()
        {
            if (inputImage == null)
            {
                MessageBox.Show("Choose input image first.", "No image", MessageBoxButton.OK);
                return;
            }
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var propertiesObject = (pgFunction.SelectedObject as FilterSettings);
            var properties = new int[] { (int)(propertiesObject.Contrast), (int)(propertiesObject.Brightness) };
            asmProxy.executeAsmContrastAndBrightness(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void doSepia()
        {
            if (inputImage == null)
            {
                MessageBox.Show("Choose input image first.", "No image", MessageBoxButton.OK);
                return;
            }
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var propertiesObject = (pgFunction.SelectedObject as FilterSettings);
            var properties = new int[] { (int)(propertiesObject.Sepia) };
            asmProxy.executeAsmSepia(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
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

            destinationPath = path;
            //if (!String.IsNullOrEmpty(path) && String.IsNullOrEmpty(destinationPath.Text))
            //{
            //    destinationPath.Text = Path.GetDirectoryName(path);
            //}
        }

        private bool pickFilePath(out string filePath, bool ensurePathExists, bool pickFolder = false)
        {
            bool picked = false;
            filePath = null;
            
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = pickFolder;
            dialog.EnsurePathExists = ensurePathExists;
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
            switch(pgFunction.SelectedProperty.Category)
            {
                case CatBlackAndWhite:
                    doBlackAndWhite();
                    break;

                case CatBlurAndSharpening:
                    doBlurAndSharpening();
                    break;

                case CatContrastAndBrightness:
                    doContrastAndBrightness();
                    break;

                case CatSepia:
                    doSepia();
                    break;

                default:
                    MessageBox.Show("Choose type of transformation!");
                    break;
            }
        }

        private void printOutputImage(BitmapSource bmpSrc)
        {
            OutputImage oi = new OutputImage();
            oi.setOutputImage(bmpSrc);
            oi.setDestinationPath(destinationPath);
            oi.Show();            
        }
    }
}
