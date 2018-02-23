using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessing;
using MatcherCore;

namespace MosaicEngine.MatchFilterStrategies
{
    public class BestMatchPerMatchFilterStrategy : IFilterStrategy<ImageMatch>
    {
        private readonly IDictionary<Point, int> _bestScores;

        public BestMatchPerMatchFilterStrategy()
        {
            _bestScores = new Dictionary<Point, int>();
        }

        public bool Matches(ImageMatch imageMatch)
        {
            int bestScore;
            if (_bestScores.TryGetValue(imageMatch.SourceOrigin, out bestScore))
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
