using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public interface IAction
    {
        IPiece Piece { get; }

        void Perform(IBoard board);
        void Undo(IBoard board);
    }
}
