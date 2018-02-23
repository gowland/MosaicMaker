using ImageProcessing.Histogram;
using ImageProcessing.ImageRegionCreationStrategies;

namespace ImageProcessing.ImageCalculations
{
    public class HistogramByRegionCalculator : StatByRegionCalculator<RgbHistogram>
    {
        private static readonly HistogramCalculator HistogramCalculator = new HistogramCalculator();

        public HistogramByRegionCalculator(IRegionCreationStrategy regionCreationStrategy)
            :base(regionCreationStrategy, HistogramCalculator.CaluclateHistogram)
        {
            
        }        
    }
}