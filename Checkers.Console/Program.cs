﻿using System.Management.Instrumentation;
using Checkers.Algorithms;
using Checkers.Engine;

namespace Checkers.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var player1 = new RandomMovePlayer();
            var player2 = new RandomMovePlayer();

            var game = new CheckersGame(player1, player2);

            game.Start();
        }
    }
}
