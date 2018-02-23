using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.Histogram;

namespace ImageProcessing.Comparison
{
/*
    public class HistogramByRegionComparer : IImageComparer
    {
        public int Compare(ImageData a, ImageData b)
        {
            int averageGreyTotalDifference = 0;

            foreach (var aPoint in a.Stats.HistogramByRegion.Keys)
            {
                RgbHistogram bScoreForSamePoint;
                if (! b.Stats.HistogramByRegion.TryGetValue(aPoint, out bScoreForSamePoint))
                    throw new Exception(string.Format("Historgram by region comparison fail: point {0}x{1} not found", aPoint.X, aPoint.Y));

                averageGreyTotalDifference += Math.Abs(a.Stats.HistogramByRegion[aPoint].Distance(bScoreForSamePoint));
            }

            return averageGreyTotalDifference;
        }
    }
*/
}
