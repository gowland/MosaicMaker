using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;

namespace MatcherEngine
{
    public class FilteredFillProviderFactory<THole, TFill> : IFillProviderFactory<THole, TFill>
    {
        private readonly IFillProvider<TFill> _fillProvider;
        private readonly IFilterStrategyFactory<THole, TFill> _filterStrategyProvider;

        public FilteredFillProviderFactory(IFillProvider<TFill> fillProvider, IFilterStrategyFactory<THole, TFill> filterStrategyProvider)
        {
            _fillProvider = fillProvider;
            _filterStrategyProvider = filterStrategyProvider;
        }

        public IFillProvider<TFill> GetFillProvider(THole hole)
        {
            return new FilteredFillProvider<TFill>(_fillProvider, _filterStrategyProvider.GetFilterStrategy(hole));
        }
    }

    public interface IFillSorter<TFill>
    {
        IEnumerable<TFill> GetSortedFills(IEnumerable<TFill> fills);
    }

    public interface IFilterStrategyFactory<in THole, in TFill>
    {
        IFilterStrategy<TFill> GetFilterStrategy(THole hole);
    }
}
