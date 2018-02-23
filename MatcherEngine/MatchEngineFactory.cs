using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;

namespace MatcherEngine
{
    public interface IMatchEngineFactory<in THole, TMatch>
    {
        IMatchEngine<THole, TMatch> GetMatchEngine();
    }

    public class MatchEngineFactory<THole, TFill, TMatch> : IMatchEngineFactory<THole, TMatch>
    {
        private readonly IFilter<TMatch> _matchFilter;
        private readonly IMatchProvider<THole, TMatch> _matchProvider; 

        public MatchEngineFactory(IMatchStrategy<THole, TFill, TMatch> matchStrategy, IFillProvider<TFill> fillProvider, IFilterStrategyFactory<THole, TFill> filterStrategyProvider, IFilter<TMatch> matchFilter)
        {
            _matchFilter = matchFilter;
            var fillProviderFactory = new FilteredFillProviderFactory<THole, TFill>(fillProvider, filterStrategyProvider);
            _matchProvider = new MatchProvider<THole, TFill, TMatch>(matchStrategy, fillProviderFactory);
        }

        public IMatchEngine<THole, TMatch> GetMatchEngine()
        {
            return new MatchEngine<THole, TMatch>(_matchProvider, _matchFilter);
        }
    }
}
