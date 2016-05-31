using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WpfUI
{
    class FilterSettings
    {
        private bool _really = true;
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

        //Dżony załatw ich
        const string CatContrastAndBrightness = "Contrast and brightness";
        const string CatSepia = "Sepia";
        const string CatBlackAndWhite = "Black and white";
        const string CatBlurAndSharpening = "Blur and sharpening";

        [DefaultValue(true)]
        [Category(CatBlackAndWhite)]
        public bool Really
        {
            get { return _really; }
            set { _really = value; }
        }

        //[DefaultValue(100)]
        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatContrastAndBrightness)]
        [Description("Type value for contrast. ")]
        public short Contrast
        {
            get { return _contrast; }
            set { _contrast = value; }
        }

        //[DefaultValue(100)]
        //[MinValue(0), MaxValue(255)]
        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatContrastAndBrightness)]
        [Description("Type value for brightness. ")]
        public short Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        //[DefaultValue(100)]
        //[MinValue(0), MaxValue(255)]
        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatSepia)]
        [Description("Type amplification for sepia. ")]
        public short Sepia
        {
            get { return _sepia; }
            set { _sepia = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for upper left element of transformation. ")]
        public short Blur11
        {
            get { return _blur11; }
            set { _blur11 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for upper center element of transformation. ")]
        public short Blur12
        {
            get { return _blur12; }
            set { _blur12 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for upper right element of transformation. ")]
        public short Blur13
        {
            get { return _blur13; }
            set { _blur13 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for middle left element of transformation. ")]
        public short Blur21
        {
            get { return _blur21; }
            set { _blur21 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for middle center element of transformation. ")]
        public short Blur22
        {
            get { return _blur22; }
            set { _blur22 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for middle right element of transformation. ")]
        public short Blur23
        {
            get { return _blur23; }
            set { _blur23 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for lower left element of transformation. ")]
        public short Blur31
        {
            get { return _blur31; }
            set { _blur31 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for lower center element of transformation. ")]
        public short Blur32
        {
            get { return _blur32; }
            set { _blur32 = value; }
        }

        [Range(typeof(short), "0", "10", ErrorMessage = "Input value between 0 and 10")]
        [Category(CatBlurAndSharpening)]
        [Description("Type amplification for lower right element of transformation. ")]
        public short Blur33
        {
            get { return _blur33; }
            set { _blur33 = value; }
        }
    }

    //[Serializable]
    //class RangeAttribute : LocationInterceptionAspect
    //{
    //    private int min;
    //    private int max;

    //    public RangeAttribute(int min, int max)
    //    {
    //        this.min = min;
    //        this.max = max;
    //    }

    //    public override void OnSetValue(LocationInterceptionArgs args)
    //    {
    //        int value = (int)args.Value;
    //        if (value < min) value = min;
    //        if (value > max) value = max;
    //        args.SetNewValue(value);
    //    }
    //}
}
