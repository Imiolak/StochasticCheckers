using System.Collections.Generic;
using System.Linq;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;
using Checkers.Engine.Extensions;

namespace Checkers.Algorithms.MTCS
{
    public class MTCSNode
    {
        private readonly IList<MTCSNode> _children;

        public MTCSNode()
        {
            _children = new List<MTCSNode>();
        }

        public MTCSNode(MTCSNode parent, IAction action) : this()
        {
            Parent = parent;
            Action = action;
        }

        public MTCSNode Parent { get; private set; }

        public IEnumerable<MTCSNode> Children => _children.AsEnumerable();

        public IAction Action { get; }

        public int WinCount { get; private set; }

        public int PlayCount { get; private set; }

        public double WinPercentage => (double) WinCount/PlayCount;

        public MTCSNode GetBestPossibleChild(IBoard board, PlayerColor activePlayer, int budget, IChildSelectionStrategy childSelectionStrategy, IChildSelectionStrategy bestChildSelectionStrategy)
        {
            _children.Clear();
            PopulateChildren(board, activePlayer);

            for (var i = 0; i < budget; i++)
            {
                var child = childSelectionStrategy.Select(_children);
                child.RunSimulation(board, activePlayer);
            }

            return bestChildSelectionStrategy.Select(_children);
        }

        private void PopulateChildren(IBoard board, PlayerColor activePlayer)
        {
            foreach (var action in board.GetValidActionsForPlayer(activePlayer))
            {
                _children.Add(new MTCSNode(this, action));
            }
        }

        private void RunSimulation(IBoard board, PlayerColor activePlayer)
        {
            Action.Perform(board);
            PlayCount++;

            if (board.EndGameConditionsMet)
            {
                WinCount++;
            }
            else
            {
                var child = GetRandomChild(board);
                WinCount += child.RunSimulationInner(board, activePlayer);
            }
            
            Action.Undo(board);
        }

        private int RunSimulationInner(IBoard board, PlayerColor activePlayer)
        {
            Action.Perform(board);
            PlayCount++;

            int result;
            if (board.EndGameConditionsMet)
            {
                if (board.GameResult == GameResult.WhiteWon && activePlayer == PlayerColor.White)
                    result = 1;
                else if (board.GameResult == GameResult.BlackWon && activePlayer == PlayerColor.Black)
                    result = 1;
                else
                    result = 0;
            }
            else
            {
                var child = GetRandomChild(board);
                result = child.RunSimulationInner(board, activePlayer);
            }

            WinCount += result;
            Action.Undo(board);
            return result;
        }

        private MTCSNode GetRandomChild(IBoard board)
        {
            var nextPlayer = board.NextPlayer;

            var action = board.GetValidActionsForPlayer(nextPlayer).Random();
            var child = _children.FirstOrDefault(c => c.Action == action);
            if (child == null)
            {
                child = new MTCSNode(this, action);
                _children.Add(child);
            }
            return child;
        }
        
        private MTCSNode GetBestChild()
        {
            return _children.OrderByDescending(c => c.WinPercentage).First();
        }
    }
}
