using System.Collections.Generic;
using System.Linq;
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
        private MTCSNode _current;

        public MTCSTree(PlayerColor playerColor, IBudgetAssignStrategy budgetAssignStrategy, IChildSelectionStrategy simulationChildSelectionStrategy, IChildSelectionStrategy bestChildSelectionStrategy)
        {
            _playerColor = playerColor;
            _budgetAssignStrategy = budgetAssignStrategy;
            _simulationChildSelectionStrategy = simulationChildSelectionStrategy;
            _bestChildSelectionStrategy = bestChildSelectionStrategy;

            _root = new MTCSNode();
            _current = _root;
        }

        public MTCSNode LastActionNode => _current;

        public IAction GetBestPossibleAction(IBoard board)
        {
            var bestChild = _current.GetBestPossibleChild(board, _playerColor, _budgetAssignStrategy.Assign(), _simulationChildSelectionStrategy, _bestChildSelectionStrategy);
            _current = bestChild;

            return bestChild.Action;
        }

        public void ConsiderPreviousPlayerActions(IEnumerable<IAction> previousPlayersActions)
        {
            foreach (var action in previousPlayersActions)
            {
                _current = _current.ProceedToNext(action);
            }
        }
    }
}
