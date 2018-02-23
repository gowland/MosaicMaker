using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatcherCore;

namespace MatcherEngine
{
    public interface IMatchEngine<in THole, TMatch>
    {
        IEnumerable<TMatch> GetMatches(IEnumerable<THole> holes);
    }

    public class MatchEngine<THole, TMatch> : IMatchEngine<THole, TMatch>
    {
        private readonly IMatchProvider<THole, TMatch> _matchProvider;
        private readonly IFilter<TMatch> _matchFilter;

        public MatchEngine(IMatchProvider<THole, TMatch> matchProvider, IFilter<TMatch> matchFilter)
        {
            _matchProvider = matchProvider;
            _matchFilter = matchFilter;
        }

        public IEnumerable<TMatch> GetMatches(IEnumerable<THole> holes)
        {
            return _matchFilter.GetFilteredItems(holes.SelectMany(_matchProvider.GetMatchesFor));
        }
    }
}
