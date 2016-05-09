using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public class MoveAction : ActionBase, IAction
    {
        private PlayerColor _previousPlayer;
        private bool _wasPreviousActionJump;

        public MoveAction(IPiece piece, int deltaRow, int deltaColumn) : base(piece, deltaRow, deltaColumn)
        {
        }

        public void Perform(IBoard board)
        {
            if (DeltaRow != 1 && DeltaRow != -1)
                throw new DeltaRowOutOfBoundsException("Delta row must be eiter 1 or -1.");
            if (DeltaColumn != 1 && DeltaColumn != -1)
                throw new DeltaColumnOutOfBoundsException("Delta column must be eiter 1 or -1.");

            var destRow = Piece.Row + DeltaRow;
            var destCol = Piece.Column + DeltaColumn;

            if (board.Pieces[destRow][destCol] != null)
                throw new DestinationCellOccupiedException();

            _previousPlayer = board.LastPlayer;
            _wasPreviousActionJump = board.WasLastActionJump;

            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;

            board.LastPlayer = Piece.Color;
            board.LastAction = this;
            board.WasLastActionJump = false;
        }

        public void Undo(IBoard board)
        {
            var destRow = Piece.Row - DeltaRow;
            var destCol = Piece.Column - DeltaColumn;

            board.Pieces[Piece.Row][Piece.Column] = null;
            board.Pieces[destRow][destCol] = Piece;
            Piece.Row = destRow;
            Piece.Column = destCol;

            board.LastPlayer = _previousPlayer;
            board.WasLastActionJump = _wasPreviousActionJump;
        }
    }
}
