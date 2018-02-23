using ImageProcessing;
using ImageProcessing.Comparison;
using MatcherCore;

namespace MosaicEngine.FillFilterStragies
{
    public class AverageDarkFillFilterStrategy : BaseImageComparerFilterStrategy
    {
        public AverageDarkFillFilterStrategy(IImageData hole, int tolerance)
            :base(new AverageGreyComparer(), hole, tolerance)
        {
        }
    }
}
