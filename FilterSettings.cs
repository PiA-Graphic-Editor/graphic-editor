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
        private short _power = 100;
        private short _contrast = 100;
        private short _brightness = 100;
        private short _sepia = 100;

        [DefaultValue(true)]
        [Category("Black&White")]
        public bool Really
        {
            get { return _really; }
            set { _really = value; }
        }

        [DefaultValue(100)]
        [Range(typeof(short), "1", "255", ErrorMessage = "Input value between 1 and 255")]
        [Category("Contrast&Brightness")]
        [Description("Type value for contrast. ")]
        public short Contrast
        {
            get { return _contrast; }
            set { _contrast = value; }
        }

        [DefaultValue(100)]
        //[MinValue(0), MaxValue(255)]
        [Range(typeof(short), "1", "255", ErrorMessage = "Input value between 1 and 255")]
        [Category("Contrast&Brightness")]
        public short Brightness
        {
            get { return _brightness; }
            set { _brightness = value; }
        }

        [DefaultValue(100)]
        //[MinValue(0), MaxValue(255)]
        [Range(typeof(short), "1", "255", ErrorMessage = "Input value between 1 and 255")]
        [Category("Sepia")]
        public short Sepia
        {
            get { return _sepia; }
            set { _sepia = value; }
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
