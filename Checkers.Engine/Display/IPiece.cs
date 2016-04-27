using System.Collections.Generic;
using Checkers.Engine.Actions;

namespace Checkers.Engine.Display
{
    public interface IPiece
    {
        PlayerColor Color { get; }
        int Column { get; set; }
        int Row { get; set; }

        IEnumerable<IAction> GetPossibleJumps(Board board);
        IEnumerable<IAction> GetPossibleMoves(Board board);
    }
}