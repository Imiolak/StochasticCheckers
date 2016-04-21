namespace Checkers.Engine.Display
{
    public static class BoardHelper
    {
        public static bool CheckRowColumnConstraints(int row, int column, int boardSize)
        {
            return row < boardSize && row >= 0 &&
                column < boardSize && column >= 0;
        }
    }
}
