using System.Collections.Generic;
using System.Linq;

namespace Checkers.Algorithms.MTCS.Strategy
{
    public class WinPercentageChildSelectionStrategy : IChildSelectionStrategy
    {
        public MTCSNode Select(IEnumerable<MTCSNode> children)
        {
            return children.OrderByDescending(c => c.WinPercentage).First();
        }
    }
}
