using System;
using Checkers.Algorithms.MTCS;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;

namespace Checkers.Algorithms
{
    public class MTCSPlayer : IPlayer
    {
        private readonly MTCSTree _mtcsTree;

        public MTCSPlayer(ISelectionStrategy selectionStrategy)
        {
            _mtcsTree = new MTCSTree(Color, selectionStrategy);
        }

        public PlayerColor Color { get; set; }

        public void PerformMove(Board board)
        {
            var action = _mtcsTree.GetBestPossibleAction(board);
            action.Perform(board);
        }
    }
}
