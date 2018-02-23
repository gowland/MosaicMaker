using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using MatcherEngine;

namespace MosaicEngine
{
    public class ImageMatchProvider
    {
        private readonly IMatchEngine<IImageData, ImageMatch> _matchEngine;

        public ImageMatchProvider(IMatchEngine<IImageData, ImageMatch> matchEngine)
        {
            _matchEngine = matchEngine;
        }

        public IEnumerable<ImageMatch> GetMatches(IEnumerable<IImageData> sourceImages)
        {
            return _matchEngine.GetMatches(sourceImages);
        }
    }
}
