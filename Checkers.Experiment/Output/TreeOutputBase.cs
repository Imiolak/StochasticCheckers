using Checkers.Algorithms.MTCS;

namespace Checkers.Experiment.Output
{
    public abstract class TreeOutputBase
    {
        protected readonly ITreeParser TreeParser;

        protected TreeOutputBase(ITreeParser treeParser)
        {
            TreeParser = treeParser;
        }

        public abstract void Write(MTCSTree tree);
    }
}
