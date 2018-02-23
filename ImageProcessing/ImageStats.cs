using System;
using System.Collections.Generic;
using ImageProcessing.Histogram;

namespace ImageProcessing
{
    [Serializable]
    public class ImageStats
    {
        public ImageStats(int averageGrey, IDictionary<Point, int> averageGreyByRegion, RgbHistogram histogram)
        {
            AverageGrey = averageGrey;
            AverageGreyByRegion = averageGreyByRegion;
            Histogram = histogram;
        }

        public int AverageGrey { get; set; }
        public IDictionary<Point, int> AverageGreyByRegion { get; set; }
        public RgbHistogram Histogram { get; set; }
    }
}