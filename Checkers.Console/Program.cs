using System.Collections.Generic;
using System.Linq.Expressions;
using Checkers.Algorithms;
using Checkers.Algorithms.MTCS.Strategy;
using Checkers.Engine;

namespace Checkers.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 20; i++)
            {
                var player1 = new MTCSPlayer(new StaticBudgetAssignStrategy(10), new RandomChildSelectionStrategy(), new WinPercentageChildSelectionStrategy());
                var player2 = new RandomMovePlayer();

                var game = new CheckersGame(player1, player2);

                game.Start();

                System.Console.WriteLine(game.Winner);
            }
        }
    }
}
