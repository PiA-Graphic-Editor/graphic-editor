using System.Runtime.InteropServices;

namespace WpfUI {
    public unsafe class AsmProxy
    {
        [DllImport("Asm.dll")]
        private static extern void asmBlackAndWhite(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters);

        [DllImport("Asm.dll")]
        private static extern void asmBlurAndSharpening(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters);

        [DllImport("Asm.dll")]
        private static extern void asmContrastAndBrightness(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters);

        [DllImport("Asm.dll")]
        private static extern void asmSepia(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters);

        public void executeAsmBlackAndWhite(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters)
        {
            asmBlackAndWhite(inputImage, outputImage, imageInfo, parameters);
        }

        public void executeAsmBlurAndSharpening(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters)
        {
            asmBlurAndSharpening(inputImage, outputImage, imageInfo, parameters);
        }

        public void executeAsmContrastAndBrightness(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters)
        {
            asmContrastAndBrightness(inputImage, outputImage, imageInfo, parameters);
        }

        public void executeAsmSepia(byte[] inputImage, byte[] outputImage, int[] imageInfo, int[] parameters)
        {
            asmSepia(inputImage, outputImage, imageInfo, parameters);
        }
    }
}