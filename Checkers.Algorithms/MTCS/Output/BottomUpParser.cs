using System.Collections.Generic;
using System.Linq;

namespace Checkers.Algorithms.MTCS.Output
{
    public class BottomUpParser : ITreeParser
    {
        public IEnumerable<MTCSNode> Parse(MTCSTree tree)
        {
            var nodes = new List<MTCSNode>();
            var node = tree.LastActionNode;

            while (node != null)
            {
                nodes.Add(node);
                node = node.Parent;
            }

            nodes.Reverse();
            return nodes.AsEnumerable();
        }
    }
}
