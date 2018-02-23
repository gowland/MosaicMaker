using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MatcherCore
{
    public interface IFilterStrategy<in T>
    {
        bool Matches(T thing);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> GetFilteredItems(IEnumerable<T> items);
    }

    public class Filter<T> : IFilter<T>
    {
        private readonly IFilterStrategy<T> _filterStrategy;

        public Filter(IFilterStrategy<T> filterStrategy)
        {
            _filterStrategy = filterStrategy;
        }

        public IEnumerable<T> GetFilteredItems(IEnumerable<T> items)
        {
            return items.Where(_filterStrategy.Matches);
        }
    }

    public class ParallelFilter<T> : IFilter<T>
    {
        private readonly IFilterStrategy<T> _filterStrategy;

        public ParallelFilter(IFilterStrategy<T> filterStrategy)
        {
            _filterStrategy = filterStrategy;
        }

        public IEnumerable<T> GetFilteredItems(IEnumerable<T> items)
        {
            return items.AsParallel().Where(_filterStrategy.Matches);
        }
    }
}
