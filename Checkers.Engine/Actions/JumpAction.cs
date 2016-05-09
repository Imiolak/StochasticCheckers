using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public class JumpAction : ActionBase, IAction
    {
        private bool _wasPerformed;

        private IPiece _beatenPiece;
        private PlayerColor _previousPlayer;
        private bool _wasPreviousActionJump;

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
            _previousPlayer = board.LastPlayer;
            _wasPreviousActionJump = board.WasLastActionJump;
            
            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[intRow][intColumn] = null;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;

            board.LastPlayer = Piece.Color;
            board.LastAction = this;
            board.WasLastActionJump = true;
            _wasPerformed = true;
        }

        public void Undo(IBoard board)
        {
            if (!_wasPerformed)
                throw new CantUndoNotPerformedAction();

            var destRow = Piece.Row - DeltaRow;
            var destCol = Piece.Column - DeltaColumn;

            var intRow = Piece.Row - DeltaRow / 2;
            var intColumn = Piece.Column - DeltaColumn / 2;
            
            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;

            board.Pieces[intRow][intColumn] = _beatenPiece;
            board.LastPlayer = _previousPlayer;
            board.WasLastActionJump = _wasPreviousActionJump;
        }
    }
}
