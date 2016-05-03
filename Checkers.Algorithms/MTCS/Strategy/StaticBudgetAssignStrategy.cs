namespace Checkers.Algorithms.MTCS.Strategy
{
    public class StaticBudgetAssignStrategy : IBudgetAssignStrategy
    {
        private readonly int _budget;

        public StaticBudgetAssignStrategy(int budget)
        {
            _budget = budget;
        }
        
        public int Assign()
        {
            return _budget;
        }
    }
}
