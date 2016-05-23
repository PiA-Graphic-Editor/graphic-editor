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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUI
{
    public partial class MainWindow : Window
    {
        private static AsmProxy asmProxy = new AsmProxy();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            //double a, b, r;
            //a = 2.0;
            //b = 3.0;
            //r = asmProxy.executeAsmAddTwoDoubles(a, b);

            double a, b, c, d, r;
            a = 2.0;
            b = 4.0;
            c = 6.0;
            d = 8.0;
            r = asmProxy.executeAsmAddFourDoubles(a, b, c, d);

            //var s = new TempStruct() { a = 2, b = 4 };
            //var r = asmProxy.executeAsmStructOperation(s);
        }

        public unsafe struct TempStruct
        {
            public double a;
            public double b;
            public double r;
        }
    }
}
