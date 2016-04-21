using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public interface IAction
    {
        void Perform(Piece piece, Board board);
    }
}
