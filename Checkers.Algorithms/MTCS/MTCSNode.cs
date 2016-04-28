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

        public int PlaysCount { get; private set; }

        public double WinPercentage => (double) WinCount/PlaysCount;

        public MTCSNode GetBestPossibleChild(IBoard board, PlayerColor playerColor, int budget, ISelectionStrategy selectionStrategy)
        {
            _children.Clear();
            PopulateChildren(board, playerColor);

            for (var i = 0; i < budget; i++)
            {
                var child = selectionStrategy.Select(_children);
                child.RunSimulation(board, playerColor);
            }

            var bestChild = GetBestChild();

            return bestChild;
        }

        private void PopulateChildren(IBoard board, PlayerColor playerColor)
        {
            foreach (var action in board.GetValidActionsForPlayer(playerColor))
            {
                _children.Add(new MTCSNode(this, action));
            }
        }

        private void RunSimulation(IBoard board, PlayerColor playerColor)
        {
            Action.Perform(board);

            if (board.EndGameConditionsMet)
            {
                WinCount++;
                PlaysCount++;
            }
            else
            {
                var result = RunSimulationInner(board, playerColor);
                WinCount += result;
                PlaysCount++;
            }

            Action.Undo(board);
        }

        private int RunSimulationInner(IBoard board, PlayerColor playerColor)
        {
            int result;

            var opponentsActions = GetOpponentsActions(board, playerColor);
            
            if (board.EndGameConditionsMet)
            {
                result = 0;
            }
            else
            {
                var action = board.GetValidActionsForPlayer(playerColor).Random();
                var performedActions = _children.Select(c => c.Action);

                if (!performedActions.Contains(action))
                    _children.Add(new MTCSNode(this, action));

                action.Perform(board);

                result = board.EndGameConditionsMet ? 1 : RunSimulationInner(board, playerColor);

                action.Undo(board);
            }

            foreach (var action in opponentsActions.Reverse())
            {
                action.Undo(board);
            }
            return result;
        }

        private IEnumerable<IAction> GetOpponentsActions(IBoard board, PlayerColor playerColor)
        {
            var actions = new List<IAction>();

            IAction action;
            do
            {
                action = board.GetValidActionsForPlayer(playerColor).Random();
                actions.Add(action);
                action.Perform(board);
            } while (action is JumpAction && !board.EndGameConditionsMet);

            return actions;
        }

        private MTCSNode GetBestChild()
        {
            return _children.OrderByDescending(c => c.WinPercentage).First();
        }
    }
}
