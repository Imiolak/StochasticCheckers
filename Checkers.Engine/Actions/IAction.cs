using Checkers.Engine.Display;

namespace Checkers.Engine.Actions
{
    public interface IAction
    {
        void Perform(IBoard board);
        void Undo(IBoard board);
    }
}
