using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;
using MosaicEngine.MatchFilterStrategies;

namespace MosaicEngine
{
    public class MatchFilterStrategy : CompositeFilterStrategy<ImageMatch>
    {
        public MatchFilterStrategy(BestMatchPerMatchFilterStrategy bestMatchPerMatchFilter)
            : base(new IFilterStrategy<ImageMatch>[]
            {
                // TODO: Filter by convolution distance
                new MaxDifferenceMatchFilterStrategy(1000000),
                bestMatchPerMatchFilter
            })
        {

        }
    }
}
