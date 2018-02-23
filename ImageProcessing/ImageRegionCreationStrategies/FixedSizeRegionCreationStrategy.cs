using System;
using System.Collections.Generic;

namespace ImageProcessing.ImageRegionCreationStrategies
{
    public interface IRegionCreationStrategy
    {
        IEnumerable<ImageRegion> GetRegions(ImageRegion sourceRegion);
    }

    public class NonOverlappingRegionCreationStrategy : FixedSizeRegionCreationStrategy
    {
        public NonOverlappingRegionCreationStrategy(int holeWidth, int holeHeight)
            :base(holeWidth, holeHeight, holeWidth, holeHeight)
        {
            
        }
    }

    public class FixedSizeRegionCreationStrategy : IRegionCreationStrategy
    {
        private readonly int _regionHeight;
        private readonly int _regionWidth;
        private readonly int _horizontalStep;
        private readonly int _verticalStep;

        public FixedSizeRegionCreationStrategy(int regionWidth, int regionHeight, int horizontalStep, int verticalStep)
        {
            _regionHeight = regionHeight;
            _regionWidth = regionWidth;
            _horizontalStep = horizontalStep;
            _verticalStep = verticalStep;
        }

        public IEnumerable<ImageRegion> GetRegions(ImageRegion sourceRegion)
        {
            if (sourceRegion.Width < _regionWidth || sourceRegion.Height < _regionHeight)
                throw new Exception(string.Format("{0}x{1} < {2}x{3}", sourceRegion.Width, sourceRegion.Height, _regionWidth, _regionHeight));

            for (int xOffset = 0; xOffset + _regionWidth < sourceRegion.Width; xOffset+= _horizontalStep)
            {
                for (int yOffset = 0; yOffset + _regionHeight < sourceRegion.Height; yOffset+= _verticalStep)
                {
                    yield return new ImageRegion(sourceRegion.X + xOffset, sourceRegion.Y + yOffset, _regionWidth, _regionHeight);
                }
            }
        }
    }
}
