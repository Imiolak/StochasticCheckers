﻿using System.Collections.Generic;
using System.Linq;
using Checkers.Algorithms.MTCS;

namespace Checkers.Experiment.Output
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
