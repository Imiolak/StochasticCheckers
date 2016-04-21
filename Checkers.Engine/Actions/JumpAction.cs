using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public class JumpAction : ActionBase, IAction
    {
        public JumpAction(int deltaRow, int deltaColumn) : base(deltaRow, deltaColumn)
        {
        }

        public void Perform(Piece piece, Board board)
        {
            if (DeltaRow != 2 && DeltaRow != -2)
                throw new ActionNotAllowedException("Delta row must be eiter 2 or -2.");
            if (DeltaColumn != 2 && DeltaColumn != -2)
                throw new ActionNotAllowedException("Delta column must be eiter 2 or -2.");

            var destRow = piece.Row + DeltaRow;
            var destCol = piece.Column + DeltaColumn;

            var intRow = DeltaRow/2 + piece.Row;
            var intColumn = DeltaColumn/2 + piece.Column;
            
            if (board.Pieces[destRow][destCol] != null)
                throw new ActionNotAllowedException("Destination cell is already occupied.");
            if (board.Pieces[intRow][intColumn] == null)
                throw new ActionNotAllowedException("There should be a piece between the destination and the jumping piece.");
            if (board.Pieces[intRow][intColumn].Color == piece.Color)
                throw new ActionNotAllowedException("The piece you want to jump over should be your oponents.");
            
            board.Pieces[piece.Row][piece.Column] = null;
            board.Pieces[intRow][intColumn] = null;
            board.Pieces[destRow][destCol] = piece;
            piece.Row = destRow;
            piece.Column = destCol;
        }
    }
}
