using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;

namespace MosaicEngine.MatchFilterStrategies
{
    public class MaxDifferenceMatchFilterStrategy : IFilterStrategy<ImageMatch>
    {
        private readonly int _maxDifference;

        public MaxDifferenceMatchFilterStrategy(int maxDifference)
        {
            _maxDifference = maxDifference;
        }

        public bool Matches(ImageMatch imageMatch)
        {
            return imageMatch.Score <= _maxDifference;
        }
    }
}
