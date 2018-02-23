using ImageProcessing;
using ImageProcessing.Comparison;
using MatcherCore;

namespace MosaicEngine.FillFilterStragies
{
    public class RgbHistogramFillFilterStrategy : BaseImageComparerFilterStrategy
    {
        public RgbHistogramFillFilterStrategy(IImageData hole, int tolerance)
            :base(new HistogramComparer(), hole, tolerance)
        {
        }
    }
}
