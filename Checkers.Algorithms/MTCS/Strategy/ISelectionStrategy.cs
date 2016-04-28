using System.Collections.Generic;

namespace Checkers.Algorithms.MTCS.Strategy
{
    public interface ISelectionStrategy
    {
        MTCSNode Select(IEnumerable<MTCSNode> children);
    }
}
