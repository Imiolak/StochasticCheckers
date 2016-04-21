namespace Checkers.Engine.Actions
{
    public abstract class ActionBase
    {
        protected ActionBase(int deltaRow, int deltaColumn)
        {
            DeltaRow = deltaRow;
            DeltaColumn = deltaColumn;
        }
        
        public int DeltaRow { get; private set; }

        public int DeltaColumn { get; private set; }
    }
}
