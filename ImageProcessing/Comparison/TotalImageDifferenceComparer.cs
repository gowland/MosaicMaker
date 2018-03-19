using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.ImageLoaders;
using UnsafeImageLibrary;

namespace ImageProcessing.Comparison
{
    public interface IImageComparer : IComparer<IImageData>
    {
//        int Compare(IImageData a, IImageData b);
    }

    public class TotalImageDifferenceComparer : IImageComparer
    {
        private readonly IImageLoader _imageLoader;

        public TotalImageDifferenceComparer(IImageLoader imageLoader)
        {
            _imageLoader = imageLoader;
        }

        public int Compare(IImageData a, IImageData b)
        {
            return CalculateDifference(a, b);
        }

        private int CalculateDifference(IImageData a, IImageData b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                throw new Exception(string.Format("Image sizes incompatible: {0} is {1}x{2}; {3} is {4}x{5}", a.ImagePath, a.X, a.Y, b.ImagePath, b.X, b.Y));

            var bitmapA = _imageLoader.LoadImage(a);
            var bitmapB = _imageLoader.LoadImage(b);

            bitmapA.Lock();
            bitmapB.Lock();

            int difference = 0;

            for (int xOffset = 0; xOffset <a.Width; xOffset++)
            {
                for (int yOffset = 0; yOffset < a.Height; yOffset++)
                {
                    Color cA = bitmapA.GetPixel(a.X + xOffset, a.Y + yOffset);
                    Color cB = bitmapB.GetPixel(b.X + xOffset, b.Y + yOffset);

                    difference += CalculatePixelColorDifference(cA, cB);
                }
            }

            bitmapA.Unlock();
            bitmapB.Unlock();

            return difference;
        }

        private int CalculateDifference_old(ImageData a, ImageData b)
        {
            if (a.Width != b.Width || a.Height != b.Height)
                throw new Exception(string.Format("Image sizes incompatible: {0} is {1}x{2}; {3} is {4}x{5}", a.ImagePath, a.X, a.Y, b.ImagePath, b.X, b.Y));

            var bitmapA = _imageLoader.LoadImage(a);
            var bitmapB = _imageLoader.LoadImage(b);

            int difference = 0;

            for (int xOffset = 0; xOffset <a.Width; xOffset++)
            {
                for (int yOffset = 0; yOffset < a.Height; yOffset++)
                {
                    Color cA = bitmapA.GetPixel(a.X + xOffset, a.Y + yOffset);
                    Color cB = bitmapB.GetPixel(b.X + xOffset, b.Y + yOffset);

                    difference += CalculatePixelColorDifference(cA, cB);
                }
            }

            return difference;
        }

        private int CalculatePixelColorDifference(Color cA, Color cB)
        {
            return Math.Abs(cA.R - cB.R)
                    + Math.Abs(cA.G - cB.G)
                    + Math.Abs(cA.B - cB.B);
        }
    }
}
