using System.Linq;
using Checkers.Engine;
using Checkers.Engine.Display;
using Checkers.Engine.Extensions;

namespace Checkers.Algorithms
{
    public class RandomMoveAlgorithmPlayer : IPlayer
    {
        public PlayerColor Color { get; set; }

        public void PerformMove(Board board, out bool wasJump)
        {
            var pieces = board.GetJumpablePiecesForPlayer(Color);

            var pieceArray = pieces as Piece[] ?? pieces.ToArray();
            if (pieceArray.Any())
            {
                var chosenPiece = pieceArray.Random();
                var chosenJump = chosenPiece.GetPossibleJumps(board).Random();

                chosenJump.Perform(chosenPiece, board);

                wasJump = true;
                return;
            }

            pieces = board.GetMovablePiecesForPlayer(Color);

            pieceArray = pieces as Piece[] ?? pieces.ToArray();
            if (pieceArray.Any())
            {
                var chosenPiece = pieceArray.Random();
                var chosenMove = chosenPiece.GetPossibleMoves(board).Random();

                chosenMove.Perform(chosenPiece, board);
            }

            wasJump = false;
        }
    }
}
