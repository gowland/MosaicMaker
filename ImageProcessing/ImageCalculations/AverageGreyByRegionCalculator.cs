using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.ImageRegionCreationStrategies;

namespace ImageProcessing.ImageCalculations
{
    public class AverageGreyByRegionCalculator : StatByRegionCalculator<int>
    {
        private static readonly AverageGreyCalculator AverageGreyCalculator = new AverageGreyCalculator();

        public AverageGreyByRegionCalculator(IRegionCreationStrategy regionCreationStrategy)
            :base(regionCreationStrategy, AverageGreyCalculator.CaluclateAverageDark)
        {
        }
    }
}
