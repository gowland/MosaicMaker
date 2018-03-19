using System.Collections.Generic;
using ImageProcessing.Histogram;
using ImageProcessing.ImageCalculations;

namespace ImageProcessing
{
    public interface IImageData
    {
        string ImagePath { get; }
        Point Origin { get; }
        int X { get; }
        int Y { get; }
        int Width { get; }
        int Height { get; }
        int AverageGrey { get; }
        IDictionary<Point, int> AverageGreyByRegion { get; }
        RgbHistogram Histogram { get; }
        ConvolutionInfo ConvolutionInfo { get; }
    }
}