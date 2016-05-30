using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfUI
{
    public class Image
    {
        public BitmapSource SourceImage { get; private set; }

        public int Stride { get; private set; }

        public int ByteLength { get; private set; }

        public int Width { get { return SourceImage.PixelWidth; } }

        public int Height { get { return SourceImage.PixelHeight; } }

        public double DpiX { get { return SourceImage.DpiX; } }

        public double DpiY { get { return SourceImage.DpiY; } }

        public PixelFormat Format { get { return SourceImage.Format; } }

        public Image(BitmapSource imageBms)
        {
            SourceImage = imageBms;
            Stride = imageBms.PixelWidth * ((imageBms.Format.BitsPerPixel + 7) / 8);
            ByteLength = Stride * imageBms.PixelHeight;
        }

        public Image(byte[] bytes, int byteLength, int width, int height, double dpiX, double dpiY, PixelFormat format)
        {
            WriteableBitmap bitmap = new WriteableBitmap(width, height, dpiX, dpiY, format, null);
            ByteLength = byteLength;
            Stride = byteLength / height;
            bitmap.WritePixels(new Int32Rect(0, 0, width, height), bytes, Stride, 0);
            SourceImage = bitmap;
        }

        public byte[] getBytes()
        {
            byte[] rgb = new byte[ByteLength];
            SourceImage.CopyPixels(rgb, Stride, 0);
            return rgb;
        }
    }
}
