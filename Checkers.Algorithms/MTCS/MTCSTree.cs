using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;

namespace Checkers.Algorithms.MTCS
{
    public class MTCSTree
    {
        private readonly PlayerColor _playerColor;
        private readonly IBudgetAssignStrategy _budgetAssignStrategy;
        private readonly IChildSelectionStrategy _simulationChildSelectionStrategy;
        private readonly IChildSelectionStrategy _bestChildSelectionStrategy;

        private MTCSNode _root;

        public MTCSTree(PlayerColor playerColor, IBudgetAssignStrategy budgetAssignStrategy, 
            IChildSelectionStrategy simulationChildSelectionStrategy, IChildSelectionStrategy bestChildSelectionStrategy)
        {
            _playerColor = playerColor;
            _budgetAssignStrategy = budgetAssignStrategy;
            _simulationChildSelectionStrategy = simulationChildSelectionStrategy;
            _bestChildSelectionStrategy = bestChildSelectionStrategy;

            _root = new MTCSNode();
        }

        public IAction GetBestPossibleAction(IBoard board)
        {
            var bestChild = _root.GetBestPossibleChild(board, _playerColor, _budgetAssignStrategy.Assign(), _simulationChildSelectionStrategy, _bestChildSelectionStrategy);
            _root = bestChild;

            return bestChild.Action;
        }
    }
}
