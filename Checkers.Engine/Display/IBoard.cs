using System.Collections.Generic;
using Checkers.Engine.Actions;

namespace Checkers.Engine.Display
{
    public interface IBoard
    {
        int BoardSize { get; }
        bool EndGameConditionsMet { get; }
        GameResult GameResult { get; }
        IPiece[][] Pieces { get; }
        PlayerColor LastPlayer { get; set; }
        PlayerColor NextPlayer { get; }
        IAction LastAction { get; set; }
        bool WasLastActionJump { get; set; }

        IEnumerable<IPiece> GetPiecesForPlayer(PlayerColor playerColor);
        IEnumerable<IAction> GetValidActionsForPlayer(PlayerColor playerColor);
    }

    public enum GameResult
    {
        WhiteWon,
        BlackWon
    }
}