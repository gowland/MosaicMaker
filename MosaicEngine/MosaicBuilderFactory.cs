using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.Comparison;
using ImageProcessing.ImageLoaders;
using ImageProcessing.ImageRegionCreationStrategies;
using MatcherCore;
using MatcherEngine;
using MosaicEngine.MatchingStrategies;

namespace MosaicEngine
{
    public class MosaicBuilderFactory
    {
        private readonly IRegionCreationStrategy _sourceRegionCreationStrategy;
        private readonly IRegionCreationStrategy _fillRegionCreationStrategy;
        private readonly IRegionCreationStrategy _averageDarkRegionCreationStrategy;
        private readonly ImageMatchProviderFactory _imageMatchProviderFactory;
        private readonly IImageLoader _imageLoader;
        private readonly ImageDataSorter<int> _imageDataSorter = new ImageDataSorter<int>(imageData => imageData.AverageGrey);

        public MosaicBuilderFactory(int holeWidth, int holeHeight, int averageDarkWidth, int averageDarkHeight, int fillHorizontalStep, int fillVerticalStep)
        {
            _imageLoader = new FlyWeightImageLoader(1000);
            _sourceRegionCreationStrategy = new NonOverlappingRegionCreationStrategy(holeWidth, holeHeight);            
            _averageDarkRegionCreationStrategy = new NonOverlappingRegionCreationStrategy(averageDarkWidth, averageDarkHeight);
            _fillRegionCreationStrategy = new FixedSizeRegionCreationStrategy(holeWidth, holeHeight, fillHorizontalStep, fillVerticalStep);
            IImageComparer imageComparer = new TotalImageDifferenceComparer(_imageLoader);
            IFilterStrategy<ImageMatch> matchFilterMatchStrategy = new MatchFilterStrategy();
            _imageMatchProviderFactory = new ImageMatchProviderFactory(
                new ImageDataMatchingStrategy(imageComparer), 
                new FillFilterStrategyFactory(), 
                new Filter<ImageMatch>(matchFilterMatchStrategy));
        }

        public MosaicBuilder GetMosaicBuilder()
        {
            return new MosaicBuilder(
               _imageLoader,
               _sourceRegionCreationStrategy,
               _fillRegionCreationStrategy,
               _averageDarkRegionCreationStrategy,
               _imageMatchProviderFactory,
               _imageDataSorter);
        }
    }

}
