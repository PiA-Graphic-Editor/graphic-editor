using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfUI
{
    /// <summary>
    /// Interaction logic for OutputImage.xaml
    /// </summary>
    public partial class OutputImage : Window
    {
        private string destinationPath = "";
        public OutputImage()
        {
            InitializeComponent();
        }

        public void setOutputImage(BitmapSource bmpSrc)
        {
            imageSource.Source = bmpSrc;
        }

        public void setDestinationPath(string path)
        {
            destinationPath = path;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonSaveFileDialog();
            dialog.DefaultDirectory = Path.GetDirectoryName(destinationPath);
            dialog.DefaultExtension = Path.GetExtension(destinationPath).Substring(1);
            dialog.DefaultFileName = String.Format("{0}_new", Path.GetFileNameWithoutExtension(destinationPath));
            dialog.EnsurePathExists = true;
            dialog.Filters.Add(new CommonFileDialogFilter("Bitmaps", "*.bmp"));
            var result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok && !String.IsNullOrEmpty(dialog.FileName)) {
                saveImage(dialog.FileName);
                this.Close();
            }
            else
            {
                this.Activate();
            }
        }

        private void saveImage(string path)
        {
            var bitmap = imageSource.Source as WriteableBitmap;
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                encoder.Save(filestream);
            }
        }
    }
}
