using ImageProcessing;
using ImageProcessing.Comparison;
using MatcherEngine;

namespace MosaicEngine.MatchingStrategies
{
    public class ImageDataMatchingStrategy : IMatchStrategy<IImageData, IImageData, ImageMatch>
    {
        private readonly IImageComparer _imageComparer;

        public ImageDataMatchingStrategy(IImageComparer imageComparer)
        {
            _imageComparer = imageComparer;
        }

        public ImageMatch GetMatch(IImageData hole, IImageData fill)
        {
            return new ImageMatch(hole, fill, _imageComparer.Compare(hole, fill));
        }
    }
}
