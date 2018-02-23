using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;

namespace MatcherEngine
{
    public interface IFilterStrategyProvider<in TFill>
    {
        IFilterStrategy<TFill> GetFilterStrategy();
    }

    public class FilteredFillProvider<TFill> : IFillProvider<TFill>
    {
        private readonly IFillProvider<TFill> _fillProvider;
        private readonly IFilter<TFill> _filter; 

        public FilteredFillProvider(IFillProvider<TFill> fillProvider, IFilterStrategy<TFill> filterStrategy)
        {
            _fillProvider = fillProvider;
            _filter = new ParallelFilter<TFill>(filterStrategy);
        }

        public IEnumerable<TFill> Fills { get { return _filter.GetFilteredItems(_fillProvider.Fills); } }
    }
}
