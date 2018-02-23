using ImageProcessing;
using ImageProcessing.Comparison;
using MatcherCore;

namespace MosaicEngine.FillFilterStragies
{
    public class AverageDarkByRegionFillFilterStrategy : BaseImageComparerFilterStrategy
    {
        public AverageDarkByRegionFillFilterStrategy(IImageData hole, int tolerance)
            :base(new AverageDarkByRegionComparer(), hole, tolerance)
        {
        }
    }
}
