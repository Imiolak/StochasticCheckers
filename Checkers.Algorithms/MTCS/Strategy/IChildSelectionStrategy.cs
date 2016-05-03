using System.Collections.Generic;

namespace Checkers.Algorithms.MTCS.Strategy
{
    public interface IChildSelectionStrategy
    {
        MTCSNode Select(IEnumerable<MTCSNode> children);
    }
}
