using System;
using System.Collections.Generic;
using System.Drawing;
using ImageProcessing.ImageRegionCreationStrategies;
using UnsafeImageLibrary;

namespace ImageProcessing.ImageCalculations
{
    public class StatByRegionCalculator<T>
    {
        private readonly IRegionCreationStrategy _regionCreationStrategy;
        private readonly Func<ImageChunk, T> _statCalculatorFunc;

        public StatByRegionCalculator(IRegionCreationStrategy regionCreationStrategy, Func<ImageChunk, T> statCalculatorFunc)
        {
            _regionCreationStrategy = regionCreationStrategy;
            _statCalculatorFunc = statCalculatorFunc;
        }

        public Dictionary<Point, T> GetStatByRegion(ImageChunk chunk)
        {
            var statByRegionDictionary = new Dictionary<Point, T>();

            foreach (ImageChunk subChunk in chunk.GetSubChunks(_regionCreationStrategy))
            {
                statByRegionDictionary[CalculateLocalizedOrigin(subChunk.Origin, chunk)] = _statCalculatorFunc(subChunk);
            }

/*
            foreach (var subRegion in _regionCreationStrategy.GetRegions(outerRegion))
            {
                statByRegionDictionary[CalculateLocalizedOrigin(subRegion.Origin, outerRegion)] = _statCalculatorFunc(new ImageChunk(bitmap, subRegion));
            }
*/

            return statByRegionDictionary;
        }

        private Point CalculateLocalizedOrigin(Point pointInRegion, ImageRegion region)
        {
            return new Point(pointInRegion.X - region.X, pointInRegion.Y - region.Y);
        }

        private Point CalculateLocalizedOrigin(Point pointInRegion, ImageChunk chunk)
        {
            return new Point(pointInRegion.X - chunk.X, pointInRegion.Y - chunk.Y);
        }
    }
}