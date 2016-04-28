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

        public void PerformMove(Board board, out bool wasJump)
        {
            var actions = board.GetValidActionsForPlayer(Color);

            var pieceArray = actions as IAction[] ?? actions.ToArray();
            if (!pieceArray.Any())
            {
                wasJump = false;
                return;
            }

            var action = pieceArray.Random();
            action.Perform(board);
            wasJump = action is JumpAction;
        }
    }
}
