using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using ImageProcessing.Comparison;

namespace MosaicEngine.MatchStatistics
{
    public class MatchStatistics
    {
        public MatchStatistics(int averageDarkComparison, int averageDarkByRegionComparison, int histogramComparison, int matchScore)
        {
            AverageDarkComparison = averageDarkComparison;
            AverageDarkByRegionComparison = averageDarkByRegionComparison;
            HistogramComparison = histogramComparison;
            MatchScore = matchScore;
        }

        public int AverageDarkComparison { get; private set; }
        public int AverageDarkByRegionComparison { get; private set; }
        public int HistogramComparison { get; private set; }
        public int MatchScore { get; private set; }
    }
}
