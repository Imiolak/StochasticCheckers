using System;
using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;

namespace Checkers.Engine.Actions
{
    public static class ActionFactory
    {
        public static IAction Create(IPiece piece, int destRow, int destColumn)
        {
            var deltaRow = destRow - piece.Row;
            var deltaColumn = destColumn - piece.Column;

            if (Math.Abs(deltaRow) == 1 && Math.Abs(deltaColumn) == 1)
                return new MoveAction(piece, deltaRow, deltaColumn);

            if (Math.Abs(deltaRow) == 2 && Math.Abs(deltaColumn) == 2)
                return new JumpAction(piece, deltaRow, deltaColumn);

            return null;
        }
    }
}
