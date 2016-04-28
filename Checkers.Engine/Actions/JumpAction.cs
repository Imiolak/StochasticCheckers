using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public class JumpAction : ActionBase, IAction
    {
        private bool _wasPerformed;
        private IPiece _beatenPiece;
        
        public JumpAction(IPiece piece, int deltaRow, int deltaColumn) : base(piece, deltaRow, deltaColumn)
        {
        }

        public void Perform(IBoard board)
        {
            if (DeltaRow != 2 && DeltaRow != -2)
                throw new DeltaRowOutOfBoundsException("Delta row must be eiter 2 or -2.");
            if (DeltaColumn != 2 && DeltaColumn != -2)
                throw new DeltaColumnOutOfBoundsException("Delta column must be eiter 2 or -2.");

            var destRow = Piece.Row + DeltaRow;
            var destCol = Piece.Column + DeltaColumn;

            var intRow = DeltaRow/2 + Piece.Row;
            var intColumn = DeltaColumn/2 + Piece.Column;
            
            if (board.Pieces[destRow][destCol] != null)
                throw new DestinationCellOccupiedException();
            if (board.Pieces[intRow][intColumn] == null)
                throw new NoPieceToJumpOverException();
            if (board.Pieces[intRow][intColumn].Color == Piece.Color)
                throw new JumpOverOwnPieceException();

            _beatenPiece = board.Pieces[intRow][intColumn];
            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[intRow][intColumn] = null;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;

            _wasPerformed = true;
        }

        public void Undo(IBoard board)
        {
            if (!_wasPerformed)
                throw new CantUndoNotPerformAction();

            var destRow = Piece.Row - DeltaRow;
            var destCol = Piece.Column - DeltaColumn;

            var intRow = DeltaRow / 2 + Piece.Row;
            var intColumn = DeltaColumn / 2 + Piece.Column;
            
            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[intRow][intColumn] = _beatenPiece;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;
        }
    }
}
