using System;
using System.Collections.Generic;
using ImageProcessing.Histogram;
using ImageProcessing.ImageCalculations;

namespace ImageProcessing
{
    [Serializable]
    public class ImageStats
    {
        public ImageStats(int averageGrey, IDictionary<Point, int> averageGreyByRegion, RgbHistogram histogram, ConvolutionInfo convolutionInfo)
        {
            AverageGrey = averageGrey;
            AverageGreyByRegion = averageGreyByRegion;
            Histogram = histogram;
            ConvolutionInfo = convolutionInfo;
        }

        public int AverageGrey { get; }
        public IDictionary<Point, int> AverageGreyByRegion { get; }
        public RgbHistogram Histogram { get; }
        public ConvolutionInfo ConvolutionInfo { get; }
    }
}