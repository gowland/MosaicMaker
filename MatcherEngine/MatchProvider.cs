using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatcherEngine
{
    public interface IMatchStrategy<in THole, in TFill, out TMatch>
    {
        TMatch GetMatch(THole hole, TFill fill);
    }

    public interface IFillProvider<out TFill>
    {
        IEnumerable<TFill> Fills { get; }         
    }   

    public interface IFillProviderFactory<in THole, out TFill>
    {
        IFillProvider<TFill> GetFillProvider(THole hole);
    }

    public interface IMatchProvider<in THole, out TMatch>
    {
        IEnumerable<TMatch> GetMatchesFor(THole hole);
    }

    public class MatchProvider<THole, TFill, TMatch> : IMatchProvider<THole, TMatch>
    {
        private readonly IMatchStrategy<THole, TFill, TMatch> _matchStrategy;
        private readonly IFillProviderFactory<THole, TFill> _fillProviderFactory;

        public MatchProvider(IMatchStrategy<THole, TFill, TMatch> matchStrategy, IFillProviderFactory<THole, TFill> fillProviderFactory)
        {
            _matchStrategy = matchStrategy;
            _fillProviderFactory = fillProviderFactory;
        }

        public IEnumerable<TMatch> GetMatchesFor(THole hole)
        {
            return _fillProviderFactory.GetFillProvider(hole).Fills.Select(fill => _matchStrategy.GetMatch(hole, fill));
        }
    }
}
