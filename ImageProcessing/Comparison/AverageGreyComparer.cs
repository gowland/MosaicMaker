using System;

namespace ImageProcessing.Comparison
{
    public class AverageGreyComparer : IImageComparer
    {
        public int Compare(IImageData a, IImageData b)
        {
            return Math.Abs(a.AverageGrey - b.AverageGrey);
        }
    }
}
