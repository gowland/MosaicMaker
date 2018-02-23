using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatterns;
using UnsafeImageLibrary;

namespace ImageProcessing.ImageLoaders
{
    public class FlyWeightImageLoader : IImageLoader
    {
        private readonly UsagePriorityFixedFlyWeight<string, FastBitmap> _flyWeight;
        private readonly IncrediblyInefficientImageLoader _incrediblyInefficientImageLoader = new IncrediblyInefficientImageLoader();

        public FlyWeightImageLoader(int capacity)
        {
            _flyWeight = new UsagePriorityFixedFlyWeight<string, FastBitmap>(capacity, LoadImageFromFile);
        }

        public FastBitmap LoadImage(IImageData imageData)
        {
            return LoadImage(imageData.ImagePath);
        }

        public FastBitmap LoadImage(string imagePath)
        {
            return _flyWeight.GetItem(imagePath);
        }

        private FastBitmap LoadImageFromFile(string imagePath)
        {
            return _incrediblyInefficientImageLoader.LoadImage(imagePath);
        }
    }
}
