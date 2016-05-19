using System.Collections.Generic;

namespace Checkers.Algorithms.MTCS.Output
{
    public interface ITreeParser
    {
        IEnumerable<MTCSNode> Parse(MTCSTree tree);
    }
}
