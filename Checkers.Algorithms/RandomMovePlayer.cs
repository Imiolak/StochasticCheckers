using System.Linq;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;
using Checkers.Engine.Extensions;

namespace Checkers.Algorithms
{
    public class RandomMovePlayer : IPlayer
    {
        public PlayerColor Color { get; set; }

        public void PerformMove(IBoard board)
        {
            var actions = board.GetValidActionsForPlayer(Color);

            var pieceArray = actions as IAction[] ?? actions.ToArray();
            if (!pieceArray.Any())
                return;

            var action = pieceArray.Random();
            action.Perform(board);
        }
    }
}
