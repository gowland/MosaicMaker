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
        public MatchStatistics(int averageDarkComparison, int averageDarkByRegionComparison, int histogramComparison, int filterComparison, int matchScore)
        {
            AverageDarkComparison = averageDarkComparison;
            AverageDarkByRegionComparison = averageDarkByRegionComparison;
            HistogramComparison = histogramComparison;
            MatchScore = matchScore;
            FilterComparison = filterComparison;
        }

        public int AverageDarkComparison { get; }
        public int AverageDarkByRegionComparison { get; }
        public int HistogramComparison { get; }
        public int FilterComparison { get; }
        public int MatchScore { get; }
    }
}
