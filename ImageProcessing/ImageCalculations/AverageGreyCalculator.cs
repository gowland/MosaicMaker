using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnsafeImageLibrary;

namespace ImageProcessing.ImageCalculations
{
    public class AverageGreyCalculator
    {
/*
        public int CaluclateAverageDark(ImageRegion region, FastBitmap bitmap)
        {
            int totalGrey = 0;

            bitmap.Lock();
            for (int xOffset = 0; xOffset < region.Width; xOffset++)
            {
                for (int yOffset = 0; yOffset < region.Height; yOffset++)
                {
                    Color color = bitmap.GetPixel(region.X + xOffset, region.Y + yOffset);

                    totalGrey += color.R + color.G + color.B;
                }
            }
            bitmap.Unlock();

            return totalGrey/(3*region.Width*region.Height);
        }
*/

        public int CaluclateAverageDark(ImageChunk chunk)
        {
            int totalGrey = 0;
            int area;

            using (ImageChunk.ImageChunkPixels pixels = chunk.LockPixels())
            {
                for (int xOffset = 0; xOffset < pixels.Width; xOffset++)
                {
                    for (int yOffset = 0; yOffset < pixels.Height; yOffset++)
                    {
//                        Color color = pixels.GetPixel(pixels.X + xOffset, pixels.Y + yOffset);
                        Color color = pixels.GetPixel(xOffset, yOffset);

                        totalGrey += color.R + color.G + color.B;
                    }
                }

                area = 3*pixels.Width*pixels.Height;
            }

            return totalGrey/area;
        }
    }
}
