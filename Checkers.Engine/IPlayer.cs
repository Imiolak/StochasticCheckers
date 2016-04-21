using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public interface IPlayer
    {
        PlayerColor Color{ get; set; }

        void PerformMove(Board board, out bool wasJump);
    }

    public enum PlayerColor
    {
        White,
        Black
    }
}