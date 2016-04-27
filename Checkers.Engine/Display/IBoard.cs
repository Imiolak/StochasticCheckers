using System.Collections.Generic;

namespace Checkers.Engine.Display
{
    public interface IBoard
    {
        int BoardSize { get; }
        bool EndGameConditionsMet { get; }
        IPiece[][] Pieces { get; }

        IEnumerable<IPiece> GetJumpablePiecesForPlayer(PlayerColor color);
        IEnumerable<IPiece> GetMovablePiecesForPlayer(PlayerColor color);
        IEnumerable<IPiece> GetPiecesForPlayer(PlayerColor playerColor);
    }
}