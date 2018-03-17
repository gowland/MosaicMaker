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
        public MatchStatistics(int averageDarkComparison, int averageDarkByRegionComparison, int histogramComparison, bool isBestMatchSame, int? bestFilterComparison, int matchScore)
        {
            AverageDarkComparison = averageDarkComparison;
            AverageDarkByRegionComparison = averageDarkByRegionComparison;
            HistogramComparison = histogramComparison;
            MatchScore = matchScore;
            IsBestMatchSame = isBestMatchSame;
            BestFilterComparison = bestFilterComparison;
        }

        public int AverageDarkComparison { get; }
        public int AverageDarkByRegionComparison { get; }
        public int HistogramComparison { get; }
        public bool IsBestMatchSame { get; }
        public int? BestFilterComparison { get; }
        public int MatchScore { get; }
    }
}
