using System;

namespace ImageProcessing.Comparison
{
    public class HistogramComparer : IImageComparer
    {
        public int Compare(IImageData a, IImageData b)
        {
            return a.Histogram.Distance(b.Histogram);
        }
    }
}
