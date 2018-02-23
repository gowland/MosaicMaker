using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.ImageCalculations;
using ImageProcessing.ImageLoaders;
using ImageProcessing.ImageRegionCreationStrategies;
using UnsafeImageLibrary;
using Point = ImageProcessing.Point;

namespace MosaicEngine
{
    public class ImageDataBuilder
    {
        private readonly string _imagePath;
        private readonly IRegionCreationStrategy _regionCreationStrategy;
        private readonly IImageLoader _imageLoader;
        private readonly AverageGreyCalculator _averageGreyCalculator = new AverageGreyCalculator();
        private readonly AverageGreyByRegionCalculator _averageGreyByRegionCalculator;
        private readonly HistogramCalculator _histogramCalculator = new HistogramCalculator();

        public ImageDataBuilder(string imagePath, IRegionCreationStrategy regionCreationStrategy, IRegionCreationStrategy subRegionCreationStrategy, IImageLoader imageLoader)
        {
            _imagePath = imagePath;
            _regionCreationStrategy = regionCreationStrategy;
            _imageLoader = imageLoader;

            _averageGreyByRegionCalculator = new AverageGreyByRegionCalculator(subRegionCreationStrategy);
        }

        public IEnumerable<ImageData> GetSourceDatas()
        {
            FastBitmap imageBitmap = _imageLoader.LoadImage(_imagePath);
            var imageRegion = new ImageRegion(Point.Origin, imageBitmap.Width, imageBitmap.Height);
            return _regionCreationStrategy.GetRegions(imageRegion).Select(region => GetImageData(region, imageBitmap));
        }

        private ImageData GetImageData(ImageRegion region, FastBitmap imageBitmap)
        {
            return new ImageData(
                    new ImageSource(_imagePath, region), 
                    GetImageStats(region, imageBitmap));
        }

        private ImageStats GetImageStats(ImageRegion region, FastBitmap imageBitmap)
        {
            var imageChunk = new ImageChunk(imageBitmap, region);

            return new ImageStats(
                    _averageGreyCalculator.CaluclateAverageDark(imageChunk),
                    _averageGreyByRegionCalculator.GetStatByRegion(imageChunk),
                    _histogramCalculator.CaluclateHistogram(imageChunk));
        }
    }
}
