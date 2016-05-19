using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public abstract class ActionBase
    {
        protected ActionBase(IPiece piece, int deltaRow, int deltaColumn)
        {
            Piece = piece;
            StartingRow = Piece.Row;
            StartingColumn = Piece.Column;
            DeltaRow = deltaRow;
            DeltaColumn = deltaColumn;
        }

        public IPiece Piece { get; }

        public int StartingRow { get; private set; }

        public int StartingColumn { get; private set; }

        public int DeltaRow { get; private set; }

        public int DeltaColumn { get; private set; }

        public override string ToString()
        {
            return $"{Piece.Color} {StartingRow}{StartingColumn} {StartingRow + DeltaRow}{StartingColumn + DeltaColumn}";
        }
    }
}
