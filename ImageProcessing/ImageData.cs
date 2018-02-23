using System;
using System.Collections.Generic;
using System.Drawing;
using ImageProcessing.Histogram;

namespace ImageProcessing
{
    [Serializable]
    public class ImageSource
    {
        private string _imagePath;
        private ImageRegion _imageRegion;

        public ImageSource(string imagePath, ImageRegion imageRegion)
        {
            _imagePath = imagePath;
            _imageRegion = imageRegion;
        }

        public string ImagePath { get { return _imagePath; } set { _imagePath = value; } }
        private ImageRegion Region { get { return _imageRegion; } set { _imageRegion = value; } }

        public Point Origin { get { return Region.Origin; } }
        public int X { get { return Region.X; } }
        public int Y { get { return Region.Y; } }
        public int Width { get { return Region.Width; } }
        public int Height { get { return Region.Height; } }
    }

    [Serializable]
    public class ImageData : IImageData
    {
        public ImageData(ImageSource source, ImageStats imageStats)
        {
            Source = source;
            Stats = imageStats;
        }

        public ImageSource Source { get; set; }
        public ImageStats Stats { get; set; }

        public string ImagePath { get { return Source.ImagePath; } }
        public Point Origin { get { return Source.Origin; } }
        public int X { get { return Source.X; } }
        public int Y { get { return Source.Y; } }
        public int Width { get { return Source.Width; } }
        public int Height { get { return Source.Height; } }
        public int AverageGrey { get { return Stats.AverageGrey; } }
        public IDictionary<Point, int> AverageGreyByRegion { get { return Stats.AverageGreyByRegion; } }
        public RgbHistogram Histogram { get { return Stats.Histogram; } }
    }
}
