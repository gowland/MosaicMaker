using System;
using System.Linq;

namespace ImageProcessing.Comparison
{
    public class HistogramComparer : IImageComparer
    {
        public int Compare(IImageData a, IImageData b)
        {
            return a.Histogram.Distance(b.Histogram);
        }
    }

    public class FilterResultComparer : IImageComparer
    {
        public int Compare(IImageData a, IImageData b)
        {
            return a.ConvolutionInfo.Scores.Zip(b.ConvolutionInfo.Scores, (a2, b2) => Math.Abs(a2 - b2)).Sum();
        }
    }
}
