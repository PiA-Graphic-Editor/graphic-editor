using System.Runtime.InteropServices;

namespace WpfUI {
    public unsafe class AsmProxy
    {
        [DllImport("Asm.dll")]
        private static extern double asmAddTwoDoubles(double a, double b);

        [DllImport("Asm.dll")]
        private static extern double asmAddFourDoubles(double a, double b, double c, double d);

        [DllImport("Asm.dll")]
        private static extern MainWindow.TempStruct* asmStructOperation(MainWindow.TempStruct* s);

        public double executeAsmAddTwoDoubles(double a, double b)
        {
            return asmAddTwoDoubles(a, b);
        }

        public double executeAsmAddFourDoubles(double a, double b, double c, double d)
        {
            return asmAddFourDoubles(a, b, c, d);
        }

        public MainWindow.TempStruct executeAsmStructOperation(MainWindow.TempStruct s)
        {
            return *asmStructOperation(&s);
        }
    }
}