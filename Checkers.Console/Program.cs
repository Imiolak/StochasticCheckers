using System.Management.Instrumentation;
using Checkers.Algorithms;
using Checkers.Engine;

namespace Checkers.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var player1 = new RandomMoveAlgorithmPlayer();
            var player2 = new RandomMoveAlgorithmPlayer();

            var game = new CheckersGame(player1, player2);

            game.Start();
        }
    }
}
