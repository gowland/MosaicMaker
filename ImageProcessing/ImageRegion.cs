using System;

namespace ImageProcessing
{
    [Serializable]
    public class ImageRegion
    {
        public ImageRegion(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public ImageRegion(Point origin, int width, int height)
        {
            X = origin.X;
            Y = origin.Y;
            Width = width;
            Height = height;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Point Origin { get {return  new Point(X, Y);} }
    }
}