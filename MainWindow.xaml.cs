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

        private Image inputImage;
        private Image outputImage;

        public MainWindow()
        {
            InitializeComponent();
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

            imageDestination.Source = outputImage.SourceImage;
        }

        private void blackAndWhite_Click(object sender, RoutedEventArgs e)
        {
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var properties = new int[] { };
            asmProxy.executeAsmBlackAndWhite(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void blurAndSharpening_Click(object sender, RoutedEventArgs e)
        {
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var properties = new int[] { };
            asmProxy.executeAsmBlurAndSharpening(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void contrastAndBrightness_Click(object sender, RoutedEventArgs e)
        {
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var properties = new int[] { };
            asmProxy.executeAsmContrastAndBrightness(inputImage.getBytes(), outputBytes, imageInfo, properties);
            createOutputImage(outputBytes);
        }

        private void sepia_Click(object sender, RoutedEventArgs e)
        {
            var outputBytes = new byte[inputImage.ByteLength];
            var imageInfo = getImageInfo(inputImage);
            var properties = new int[] { };
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

            if (!String.IsNullOrEmpty(path) && String.IsNullOrEmpty(destinationPath.Text))
            {
                destinationPath.Text = Path.GetDirectoryName(path);
            }
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
    }
}
