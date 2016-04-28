using System.Collections.Generic;
using Checkers.Engine.Extensions;

namespace Checkers.Algorithms.MTCS.Strategy
{
    public class RandomSelectionStrategy : ISelectionStrategy
    {
        public MTCSNode Select(IEnumerable<MTCSNode> children)
        {
            return children.Random();
        }
    }
}
