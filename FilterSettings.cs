using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WpfUI
{
    class FilterSettings
    {
        private bool _blacknWhite = false;
        private short _contrast = 0;
        private short _brightness = 0;
        private short _sepia = 0;
        private short _blur11 = 0;
        private short _blur12 = 0;
        private short _blur13 = 0;
        private short _blur21 = 0;
        private short _blur22 = 0;
        private short _blur23 = 0;
        private short _blur31 = 0;
        private short _blur32 = 0;
        private short _blur33 = 0;

        [DefaultValue(true)]
        [Category(CategoriesNames.BlackAndWhite)]
        [Description("No additional parameters required. ")]
        public bool Transform
        {
            get { return _blacknWhite; }
            set { _blacknWhite = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.ContrastAndBrightness)]
        [Description("Type value for contrast. ")]
        public short Contrast
        {
            get { return _contrast; }
            set { _contrast = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.ContrastAndBrightness)]
        [Description("Type value for brightness. ")]
        public short Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.Sepia)]
        [Description("Type amplification for sepia. ")]
        public short Sepia
        {
            get { return _sepia; }
            set { _sepia = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for upper left element of transformation. ")]
        public short Blur11
        {
            get { return _blur11; }
            set { _blur11 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for upper center element of transformation. ")]
        public short Blur12
        {
            get { return _blur12; }
            set { _blur12 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for upper right element of transformation. ")]
        public short Blur13
        {
            get { return _blur13; }
            set { _blur13 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for middle left element of transformation. ")]
        public short Blur21
        {
            get { return _blur21; }
            set { _blur21 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for middle center element of transformation. ")]
        public short Blur22
        {
            get { return _blur22; }
            set { _blur22 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for middle right element of transformation. ")]
        public short Blur23
        {
            get { return _blur23; }
            set { _blur23 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for lower left element of transformation. ")]
        public short Blur31
        {
            get { return _blur31; }
            set { _blur31 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for lower center element of transformation. ")]
        public short Blur32
        {
            get { return _blur32; }
            set { _blur32 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CategoriesNames.BlurAndSharpening)]
        [Description("Type amplification for lower right element of transformation. ")]
        public short Blur33
        {
            get { return _blur33; }
            set { _blur33 = value; }
        }
    }
}
