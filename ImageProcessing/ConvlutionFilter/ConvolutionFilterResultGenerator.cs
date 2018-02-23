using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using CommonUtilities;
using UnsafeImageLibrary;

namespace ImageProcessing.ConvlutionFilter
{
    public class ConvolutionFilterResultGenerator
    {
        public static int[,] ApplyFilter(ImageChunk wholeChunk, ConvolutionFilterMatrix filter)
        {
            int[,] accumlator = new int[wholeChunk.Width, wholeChunk.Height];

            using (var pixels = wholeChunk.LockPixels())
            {
                for (int y = 0; y < wholeChunk.Height; y++)
                {
                    for (int x = 0; x < wholeChunk.Width; x++)
                    {
                        for (int filterXOffset = 0; filterXOffset < filter.Width; filterXOffset++)
                        {
                            for (int filterYOffset = 0; filterYOffset < filter.Height; filterYOffset++)
                            {
                                int pixelXOffset = x + filterXOffset - 1;
                                int pixelYOffset = y + filterYOffset - 1;

                                if (pixelXOffset >= 0 && pixelXOffset < wholeChunk.Width && pixelYOffset >= 0 &&
                                    pixelYOffset < wholeChunk.Height)
                                {
                                    Color pixel = pixels.GetPixel(pixelXOffset, pixelYOffset);
                                    int gray = (pixel.R + pixel.G + pixel.B) / 3;

                                    accumlator[x, y] += (int) filter.Multiplier.Multiply((filter[filterXOffset, filterYOffset] * gray));
                                }
                            }
                        }
                    }
                }
            }

            return accumlator;
        }

        public static FastBitmap ResultToBitmap(int[,] accumlator)
        {
            Bitmap r = new Bitmap(accumlator.GetLength(0), accumlator.GetLength(1));
            FastBitmap resultBitmap = new FastBitmap(r);
            resultBitmap.Clear(Color.Black);

            using (resultBitmap.Lock())
            {
                accumlator.Foreach((x, y, value) =>
                {
                    int accumulatedValue = accumlator[x, y];
                    int newGray = accumulatedValue < 0 ? 0 : (accumulatedValue > 255 ? 255 : accumulatedValue);
                    resultBitmap.SetPixel(x, y, Color.FromArgb(255, newGray, newGray, newGray));
                });

/*
                for (int y = 0; y < wholeChunk.Height; y++)
                {
                    for (int x = 0; x < wholeChunk.Width; x++)
                    {
                        int accumulatedValue = accumlator[x, y];
                        int newGray = accumulatedValue < 0 ? 0 : (accumulatedValue > 255 ? 255 : accumulatedValue);
                        resultBitmap.SetPixel(x, y, Color.FromArgb(255, newGray, newGray, newGray));
                    }
                }
*/
            }

            return resultBitmap;
        }
    }
}