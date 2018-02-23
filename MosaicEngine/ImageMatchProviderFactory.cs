using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using MatcherCore;
using MatcherEngine;

namespace MosaicEngine
{
    public class ImageMatchProviderFactory
    {
        private readonly IMatchStrategy<IImageData, IImageData, ImageMatch> _matchStrategy;
        private readonly IFilterStrategyFactory<IImageData, IImageData> _filterStrategyFactory;
        private readonly IFilter<ImageMatch> _matchFilter;

        public ImageMatchProviderFactory(
            IMatchStrategy<IImageData, IImageData, ImageMatch> matchStrategy, 
            IFilterStrategyFactory<IImageData, IImageData> filterStrategyFactory,
            IFilter<ImageMatch> matchFilter)
        {
            _matchStrategy = matchStrategy;
            _filterStrategyFactory = filterStrategyFactory;
            _matchFilter = matchFilter;
        }

        public ImageMatchProvider GetImageMatchProvider(IFillProvider<IImageData> fillProvider, IImageDataSorter imageDataSorter)
        {
            IFillProviderFactory<IImageData, IImageData> fillProviderFactory = new FilteredFillProviderFactory<IImageData, IImageData>(fillProvider, _filterStrategyFactory);
            IMatchProvider<IImageData, ImageMatch> matchProvider = new MatchProvider<IImageData, IImageData, ImageMatch>(
                _matchStrategy, fillProviderFactory);
            IMatchEngine<IImageData, ImageMatch> matchEngine = new MatchEngine<IImageData, ImageMatch>(matchProvider, _matchFilter);
            return new ImageMatchProvider(matchEngine);
        }
    }
}
