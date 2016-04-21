using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public class MoveAction : ActionBase, IAction
    {
        public MoveAction(int deltaRow, int deltaColumn) : base(deltaRow, deltaColumn)
        {
        }

        public void Perform(Piece piece, Board board)
        {
            if (DeltaRow != 1 && DeltaRow != -1)
                throw new ActionNotAllowedException("Delta row must be eiter 1 or -1.");
            if (DeltaColumn != 1 && DeltaColumn != -1)
                throw new ActionNotAllowedException("Delta column must be eiter 1 or -1.");

            var destRow = piece.Row + DeltaRow;
            var destCol = piece.Column + DeltaColumn;

            if (board.Pieces[destRow][destCol] != null)
                throw new ActionNotAllowedException("Destination cell is already occupied.");

            board.Pieces[piece.Row][piece.Column] = null;
            board.Pieces[destRow][destCol] = piece;
            piece.Row = destRow;
            piece.Column = destCol;
        }
    }
}
