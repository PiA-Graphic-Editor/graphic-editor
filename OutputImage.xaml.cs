using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

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
            //imageSource = new Image(bmpSrc);
            return;
        }

        public void setDestinationPath(string path)
        {
            destinationPath = path;
            return;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
