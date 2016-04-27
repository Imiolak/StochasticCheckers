﻿using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public abstract class ActionBase
    {
        protected IPiece Piece;

        protected ActionBase(IPiece piece, int deltaRow, int deltaColumn)
        {
            Piece = piece;
            DeltaRow = deltaRow;
            DeltaColumn = deltaColumn;
        }
        
        public int DeltaRow { get; private set; }

        public int DeltaColumn { get; private set; }
    }
}
