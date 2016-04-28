using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;

namespace Checkers.Algorithms.MTCS
{
    public class MTCSTree
    {
        private const int Budget = 10;

        private readonly PlayerColor _playerColor;
        private readonly ISelectionStrategy _selectionStrategy;

        private MTCSNode _root;

        public MTCSTree(PlayerColor playerColor, ISelectionStrategy selectionStrategy)
        {
            _playerColor = playerColor;
            _selectionStrategy = selectionStrategy;

            _root = new MTCSNode();
        }

        public IAction GetBestPossibleAction(IBoard board)
        {
            var bestChild = _root.GetBestPossibleChild(board, _playerColor, Budget, _selectionStrategy);
            _root = bestChild;

            return bestChild.Action;
        }
    }
}
