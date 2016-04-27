using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public interface IUndoableAction
    {
        void Undo(IBoard board);
    }
}
