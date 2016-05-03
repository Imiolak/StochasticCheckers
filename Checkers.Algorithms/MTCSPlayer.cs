using Checkers.Algorithms.MTCS;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Display;

namespace Checkers.Algorithms
{
    public class MTCSPlayer : IPlayer
    {
        private readonly MTCSTree _mtcsTree;

        public MTCSPlayer(IBudgetAssignStrategy budgetAssignStrategy, IChildSelectionStrategy simulationChildSelectionStrategy, IChildSelectionStrategy bestChildSelectionStrategy)
        {
            _mtcsTree = new MTCSTree(Color, budgetAssignStrategy, simulationChildSelectionStrategy, bestChildSelectionStrategy);
        }

        public PlayerColor Color { get; set; }

        public void PerformMove(Board board)
        {
            var action = _mtcsTree.GetBestPossibleAction(board);
            action.Perform(board);
        }
    }
}
