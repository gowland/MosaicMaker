using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing.Comparison;
using ImageProcessing.ImageLoaders;
using MosaicEngine.MatchFilterStrategies;

namespace MosaicEngine.MatchStatistics
{
    public class MatchStatisticBuilder
    {
        private readonly IImageLoader _imageLoader;
        readonly AverageGreyComparer _averageGreyComparer = new AverageGreyComparer();
        readonly AverageDarkByRegionComparer _averageDarkByRegionComparer = new AverageDarkByRegionComparer();
        readonly HistogramComparer _histogramComparer = new HistogramComparer();

        public MatchStatisticBuilder(IImageLoader imageLoader)
        {
            _imageLoader = imageLoader;
        }

        public void WriteStatistics(IEnumerable<ImageMatch> matches, BestMatchPerMatchFilterStrategy bestMatchFilter)
        {
            WriteSummary(GetMatchStatistics(matches), bestMatchFilter);
        }

        private IEnumerable<MatchStatistics> GetMatchStatistics(IEnumerable<ImageMatch> matches)
        {
            return matches.Select(match => new MatchStatistics(
                    _averageGreyComparer.Compare(match.Source, match.Fill),
                    _averageDarkByRegionComparer.Compare(match.Source, match.Fill),
                    _histogramComparer.Compare(match.Source, match.Fill),
                    match.Source.ConvolutionInfo.Scores.Zip(match.Fill.ConvolutionInfo.Scores, (a,b)=>Math.Abs(a-b)).Sum(),
                    match.Score));
        }

        private bool FilterIndexIsEquivalent(int a, int b)
        {
            return ((a == 0 || a == 1) && (b == 0 || b == 1))
                   || ((a == 2 || a == 3) && (b == 2 || b == 3));
        }

        private void WriteSummary(IEnumerable<MatchStatistics> statistics, BestMatchPerMatchFilterStrategy bestMatchFilter)
        {
            IList<MatchStatistics> statisticsList = statistics.ToList();
            Console.WriteLine("Min average dark {0}", statisticsList.Select(stat => stat.AverageDarkComparison).Min());
            Console.WriteLine("Max average dark {0}", statisticsList.Select(stat => stat.AverageDarkComparison).Max());
            Console.WriteLine("Average average dark {0}", statisticsList.Select(stat => stat.AverageDarkComparison).Average());
            Console.WriteLine("StdDev average dark {0}", StdDev(statisticsList.Select(stat => stat.AverageDarkComparison)));

            Console.WriteLine("Min average dark by region {0}", statisticsList.Select(stat => stat.AverageDarkByRegionComparison).Min());
            Console.WriteLine("Max average dark by region {0}", statisticsList.Select(stat => stat.AverageDarkByRegionComparison).Max());
            Console.WriteLine("Average average dark by region {0}", statisticsList.Select(stat => stat.AverageDarkByRegionComparison).Average());
            Console.WriteLine("StdDev average dark by region {0}", StdDev(statisticsList.Select(stat => stat.AverageDarkByRegionComparison)));

            Console.WriteLine("Min histogram difference {0}", statisticsList.Select(stat => stat.HistogramComparison).Min());
            Console.WriteLine("Max histogram difference {0}", statisticsList.Select(stat => stat.HistogramComparison).Max());
            Console.WriteLine("Average histogram difference {0}", statisticsList.Select(stat => stat.HistogramComparison).Average());
            Console.WriteLine("StdDev histogram difference {0}", StdDev(statisticsList.Select(stat => stat.HistogramComparison)));


/*
            Console.WriteLine("Min filter difference {0}", statisticsList.Select(stat => stat.FilterComparison).Min());
            Console.WriteLine("Max filter difference {0}", statisticsList.Select(stat => stat.FilterComparison).Max());
            Console.WriteLine("Average filter difference {0}", statisticsList.Select(stat => stat.FilterComparison).Average());
            Console.WriteLine("StdDev filter difference {0}", StdDev(statisticsList.Select(stat => stat.FilterComparison)));
*/

            var convolutionEfficacies = bestMatchFilter.BestMatchEfficacies.ToList();
            Console.WriteLine("Min filter difference {0}", convolutionEfficacies.Min());
            Console.WriteLine("Max filter difference {0}", convolutionEfficacies.Max());
            Console.WriteLine("Average filter difference {0}", convolutionEfficacies.Average());
            Console.WriteLine("StdDev filter difference {0}", StdDev(convolutionEfficacies));

            var matchCounts = bestMatchFilter.TotalMatches.ToList();
            Console.WriteLine("Min filter difference {0}", matchCounts.Min());
            Console.WriteLine("Max filter difference {0}", matchCounts.Max());
            Console.WriteLine("Average filter difference {0}", matchCounts.Average());
            Console.WriteLine("StdDev filter difference {0}", StdDev(matchCounts));

            Console.WriteLine("Min match score {0}", statisticsList.Select(stat => stat.MatchScore).Min());
            Console.WriteLine("Max match score {0}", statisticsList.Select(stat => stat.MatchScore).Max());
            Console.WriteLine("Average match score {0}", statisticsList.Select(stat => stat.MatchScore).Average());
            Console.WriteLine("StdDev match score {0}", StdDev(statisticsList.Select(stat => stat.MatchScore)));
        }

        private static double StdDev(IEnumerable<int> intValues)
        {
            IList<double> values = intValues.Select(val => (double) val).ToList();
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }
    }
}
