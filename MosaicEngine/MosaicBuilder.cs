using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.ImageLoaders;
using ImageProcessing.ImageRegionCreationStrategies;
using MatcherEngine;
using MosaicEngine.MatchStatistics;

namespace MosaicEngine
{
    public class MosaicBuilder
    {
        private const double NewImagePercentage = 0.9;
        private readonly IRegionCreationStrategy _sourceRegionCreationStrategy;
        private readonly IRegionCreationStrategy _fillRegionCreationStrategy;
        private readonly IRegionCreationStrategy _averageGreyRegionCreationStrategy;
        private readonly ImageMatchProviderFactory _imageMatchProviderFactory;
        private readonly IImageDataSorter _imageDataSorter;
        private readonly IImageLoader _imageLoader;

        public MosaicBuilder(
            IImageLoader imageLoader, 
            IRegionCreationStrategy sourceRegionCreationStrategy, 
            IRegionCreationStrategy fillRegionCreationStrategy, 
            IRegionCreationStrategy averageGreyRegionCreationStrategy, 
            ImageMatchProviderFactory imageMatchProviderFactory, 
            IImageDataSorter imageDataSorter)
        {
            _imageLoader = imageLoader;
            _sourceRegionCreationStrategy = sourceRegionCreationStrategy;
            _fillRegionCreationStrategy = fillRegionCreationStrategy;
            _averageGreyRegionCreationStrategy = averageGreyRegionCreationStrategy;
            _imageMatchProviderFactory = imageMatchProviderFactory;
            _imageDataSorter = imageDataSorter;
        }

        public void CreateMosaic(string source, string fillDirectory, string output, string indexFile = null)
        {
            var sourceBitmap = (Bitmap)Image.FromFile(source);

            // Get fill provider
            IFillProvider<IImageData> fillProvider= new RecursiveDirectoryFillProvider(fillDirectory, _fillRegionCreationStrategy, _averageGreyRegionCreationStrategy, _imageLoader);

            // Get match provider
            ImageMatchProvider matchProvider = _imageMatchProviderFactory.GetImageMatchProvider(fillProvider, _imageDataSorter);

            // Get source provider
            var sourceProvider = new ImageDataBuilder(
                source, 
                _sourceRegionCreationStrategy,
                _averageGreyRegionCreationStrategy, 
                _imageLoader);

            // Reconstruct
            var imageBuilder = new ReconstructedImageBuilder(sourceBitmap.Width, sourceBitmap.Height, _imageLoader, NewImagePercentage);
            IList<ImageMatch> matches =
                matchProvider.GetMatches(_imageDataSorter.GetSortedImageDatas(sourceProvider.GetSourceDatas())).ToList();
            imageBuilder.ApplyMatches(matches);
            imageBuilder.SaveAs(output);

            // Build stats
            var statsBuilder = new MatchStatisticBuilder(_imageLoader);
            statsBuilder.WriteStatistics(matches);
        }
    }
}
