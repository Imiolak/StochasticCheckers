using System.Collections.Generic;
using Checkers.Engine.Actions;

namespace Checkers.Engine.Display
{
    public interface IBoard
    {
        int BoardSize { get; }
        bool EndGameConditionsMet { get; }
        IPiece[][] Pieces { get; }

        PlayerColor NextPlayer();
        IEnumerable<IPiece> GetPiecesForPlayer(PlayerColor playerColor);
        IEnumerable<IAction> GetValidActionsForPlayer(PlayerColor playerColor);
    }
}