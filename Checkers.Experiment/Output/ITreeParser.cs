using System.Collections.Generic;
using Checkers.Algorithms.MTCS;

namespace Checkers.Experiment.Output
{
    public interface ITreeParser
    {
        IEnumerable<MTCSNode> Parse(MTCSTree tree);
    }
}
