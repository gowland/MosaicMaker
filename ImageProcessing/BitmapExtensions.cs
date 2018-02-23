using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnsafeImageLibrary;

namespace ImageProcessing
{
    public static class BitmapExtensions
    {
        public static IEnumerable<Color> AllColors(this FastBitmap image, ImageRegion region)
        {
            image.Lock();
            for (int xOffset = 0; xOffset < region.Width; xOffset++)
            {
                for (int yOffset = 0; yOffset < region.Height; yOffset++)
                {
                    yield return image.GetPixel(region.X + xOffset, region.Y + yOffset);
                }
            }
            image.Unlock();
        }
    }
}
