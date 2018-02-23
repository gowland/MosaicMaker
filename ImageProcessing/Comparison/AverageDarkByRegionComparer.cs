using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageProcessing.Comparison
{
    public class AverageDarkByRegionComparer : IImageComparer
    {
        public int Compare(IImageData a, IImageData b)
        {
            int averageGreyTotalDifference = 0;

            foreach (var aPoint in a.AverageGreyByRegion.Keys)
            {
                int bScoreForSamePoint;
                if (! b.AverageGreyByRegion.TryGetValue(aPoint, out bScoreForSamePoint))
                    throw new Exception(string.Format("Average grey comparison fail: point {0}x{1} not found", aPoint.X, aPoint.Y));

                averageGreyTotalDifference += Math.Abs(a.AverageGreyByRegion[aPoint] - bScoreForSamePoint);
            }

            return averageGreyTotalDifference;
        }
    }
}
