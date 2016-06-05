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

        public byte[] executeAsmBlackAndWhite(byte[] inputImage, int[] imageInfo, int[] parameters)
        {
            byte[] outputImage = new byte[imageInfo[2]];
            asmBlackAndWhite(inputImage, outputImage, imageInfo, parameters);

            return outputImage;
        }

        public byte[] executeAsmBlurAndSharpening(byte[] inputImage, int[] imageInfo, int[] parameters)
        {
            byte[] outputImage = new byte[imageInfo[2]];
            asmBlurAndSharpening(inputImage, outputImage, imageInfo, parameters);

            return outputImage;
        }

        public byte[] executeAsmContrastAndBrightness(byte[] inputImage, int[] imageInfo, int[] parameters)
        {
            byte[] outputImage = new byte[imageInfo[2]];
            asmContrastAndBrightness(inputImage, outputImage, imageInfo, parameters);

            return outputImage;
        }

        public byte[] executeAsmSepia(byte[] inputImage, int[] imageInfo, int[] parameters)
        {
            byte[] outputImage = new byte[imageInfo[2]];
            asmSepia(inputImage, outputImage, imageInfo, parameters);

            return outputImage;
        }
    }
}