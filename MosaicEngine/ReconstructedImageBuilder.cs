using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using ImageProcessing;
using ImageProcessing.ImageLoaders;
using UnsafeImageLibrary;

namespace MosaicEngine
{
    public class ReconstructedImageBuilder
    {
        private readonly IImageLoader _imageLoader;
        private readonly double _newImagePercentage;
        private readonly double _oldImagePercentage;
        private readonly Bitmap _newImage;

        public ReconstructedImageBuilder(int width, int height, IImageLoader imageLoader, double newImagePercentage)
        {
            _imageLoader = imageLoader;
            _newImagePercentage = newImagePercentage;
            _oldImagePercentage = (1 - _newImagePercentage);
            _newImage = new Bitmap(width, height);
        }

        public void SaveAs(string path)
        {
            _newImage.Save(path);
        }

        public void ApplyMatches(IEnumerable<ImageMatch> matches)
        {
            foreach (var imageMatch in matches)
            {
                WriteSourceAndFill(imageMatch.Source, imageMatch.Fill);
            }
        }

        private void WriteSourceAndFill(IImageData hole, IImageData fill)
        {
            FastBitmap holeImage = _imageLoader.LoadImage(hole);
            FastBitmap fillImage = _imageLoader.LoadImage(fill);

            holeImage.Lock();
            fillImage.Lock();

            for (int xOffset = 0; xOffset < hole.Width; xOffset++)
            {
                for (int yOffset = 0; yOffset < hole.Height; yOffset++)
                {
                    Color holeColor = holeImage.GetPixel(hole.X + xOffset, hole.Y + yOffset);
                    Color fillColor = fillImage.GetPixel(fill.X + xOffset, fill.Y + yOffset);
                    _newImage.SetPixel(hole.X + xOffset, hole.Y + yOffset, GetNewColor(holeColor, fillColor));
                }
            }

            holeImage.Unlock();
            fillImage.Unlock();
        }

        private Color GetNewColor(Color source, Color fill)
        {
            return Color.FromArgb(
                255, 
                GetNewImageValue(source.R, fill.R),
                GetNewImageValue(source.G, fill.G),
                GetNewImageValue(source.B, fill.B));
        }

        private int GetNewImageValue(int source, int fill)
        {
            return (int)Math.Floor((source*_oldImagePercentage) + (fill*_newImagePercentage));
        }
    }
}
