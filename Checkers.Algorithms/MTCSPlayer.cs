using Checkers.Algorithms.MTCS;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Display;

namespace Checkers.Algorithms
{
    public class MTCSPlayer : IPlayer
    {
        private readonly IBudgetAssignStrategy _budgetAssignStrategy;
        private readonly IChildSelectionStrategy _simulationChildSelectionStrategy;
        private readonly IChildSelectionStrategy _bestChildSelectionStrategy;

        private MTCSTree _mtcsTree;
        
        public MTCSPlayer(IBudgetAssignStrategy budgetAssignStrategy, IChildSelectionStrategy simulationChildSelectionStrategy, IChildSelectionStrategy bestChildSelectionStrategy)
        {
            _budgetAssignStrategy = budgetAssignStrategy;
            _simulationChildSelectionStrategy = simulationChildSelectionStrategy;
            _bestChildSelectionStrategy = bestChildSelectionStrategy;
        }

        public PlayerColor Color { get; set; }

        public void PerformMove(IBoard board)
        {
            if (_mtcsTree == null)
                _mtcsTree = new MTCSTree(Color, _budgetAssignStrategy, _simulationChildSelectionStrategy, _bestChildSelectionStrategy);

            var action = _mtcsTree.GetBestPossibleAction(board);
            action.Perform(board);
        }
    }
}
