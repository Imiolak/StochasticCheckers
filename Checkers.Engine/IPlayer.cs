using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public interface IPlayer
    {
        PlayerColor Color{ get; set; }

        void PerformMove(IBoard board);
    }

    public enum PlayerColor
    {
        White,
        Black
    }
}