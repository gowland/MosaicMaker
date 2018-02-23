using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcherCore
{
    public class CompositeFilterStrategy<T> : IFilterStrategy<T>
    {
        private readonly IEnumerable<IFilterStrategy<T>> _filterStrategies;

        public CompositeFilterStrategy(IEnumerable<IFilterStrategy<T>> filterStrategies)
        {
            _filterStrategies = filterStrategies;
        }

        public bool Matches(T thing)
        {
            return _filterStrategies.All(strategy => strategy.Matches(thing));
        }
    }
}
