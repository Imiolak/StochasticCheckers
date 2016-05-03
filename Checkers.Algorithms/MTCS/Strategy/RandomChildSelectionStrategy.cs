using System.Collections.Generic;
using Checkers.Engine.Extensions;

namespace Checkers.Algorithms.MTCS.Strategy
{
    public class RandomChildSelectionStrategy : IChildSelectionStrategy
    {
        public MTCSNode Select(IEnumerable<MTCSNode> children)
        {
            return children.Random();
        }
    }
}
