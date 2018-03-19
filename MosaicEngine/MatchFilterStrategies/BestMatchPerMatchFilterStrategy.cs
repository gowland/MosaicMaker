using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using MatcherCore;

namespace MosaicEngine.MatchFilterStrategies
{
    public class BestMatchCorrelationEvaluator
    {
        private int yesCount;
        private int total;

        public void RecordYes()
        {
            ++yesCount;
            ++total;
        }
        public void RecordNo()
        {
            ++total;
        }

        public double PercentYes => (double)yesCount / (double)total;
    }

    public class BestMatchPerMatchFilterStrategy : IFilterStrategy<ImageMatch>
    {
        private readonly IDictionary<Point, ImageMatch> _bestScores = new Dictionary<Point, ImageMatch>();
        private readonly IDictionary<Point, IList<int>> _bestConvolutionMatches = new Dictionary<Point, IList<int>>();

        public bool Matches(ImageMatch imageMatch)
        {
            if (_bestScores.TryGetValue(imageMatch.SourceOrigin, out ImageMatch bestScore))
            {
                if (imageMatch.Score >= bestScore.Score)
                {
                    return false;
                }
            }

            _bestScores[imageMatch.SourceOrigin] = imageMatch;

            int convolutionDifference = GetConvolutionDifference(imageMatch);
            if (!_bestConvolutionMatches.ContainsKey(imageMatch.SourceOrigin))
            {
                _bestConvolutionMatches[imageMatch.SourceOrigin] = new List<int>() {convolutionDifference};
            }
            else
            {
                _bestConvolutionMatches[imageMatch.SourceOrigin].Add(convolutionDifference);} 
            return true;
        }

        private static int GetConvolutionDifference(ImageMatch imageMatch)
        {
            return imageMatch.Source.ConvolutionInfo.Difference(imageMatch.Fill.ConvolutionInfo);
        }

        public IEnumerable<int> BestMatchEfficacies
        {
            get
            {
                foreach (Point point in _bestScores.Keys)
                {
                    int bestMatchConvolutionDifference = GetConvolutionDifference(_bestScores[point]);
                    yield return _bestConvolutionMatches[point]
                        .OrderBy(v => v)
                        .TakeWhile(v => v < bestMatchConvolutionDifference)
                        .Count();
                }
            }
        }

        public IEnumerable<int> TotalMatches
        {
            get { return _bestConvolutionMatches.Select(m => m.Value.Count); }
        }
    }

    public class BestMatchPerMatchFilterStrategyWithNoEvaluation : IFilterStrategy<ImageMatch>
    {
        private readonly IDictionary<Point, int> _bestScores;

        public BestMatchPerMatchFilterStrategyWithNoEvaluation ()
        {
            _bestScores = new Dictionary<Point, int>();
        }

        public bool Matches(ImageMatch imageMatch)
        {
            if (_bestScores.TryGetValue(imageMatch.SourceOrigin, out int bestScore))
            {
                if (imageMatch.Score >= bestScore)
                {
                    return false;
                }
            }

            _bestScores[imageMatch.SourceOrigin] = imageMatch.Score;
            return true;
        }
    }
}
