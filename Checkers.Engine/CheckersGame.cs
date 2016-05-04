using System;
using System.Collections.Generic;
using System.Diagnostics;
using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public class CheckersGame : IGame
    {
        private readonly IDictionary<PlayerColor, IPlayer> _players;
        private readonly Board _board;
        private readonly Stopwatch _stopwatch;

        public CheckersGame(IPlayer player1, IPlayer player2)
        {
            _players = new Dictionary<PlayerColor, IPlayer>
            {
                [PlayerColor.White] = player1,
                [PlayerColor.Black] = player2
            };
            player1.Color = PlayerColor.White;
            player2.Color = PlayerColor.Black;
            
            _board = new Board();
            _stopwatch = new Stopwatch();
        }

        public GameResult Result => _board.GameResult;

        public TimeSpan TimeElapsed => _stopwatch.Elapsed;

        public void Start(bool debug = false)
        {
            InitializeGame();
            while (!_board.EndGameConditionsMet)
            {
                var nextPlayer = _players[_board.NextPlayer];
                Debug(debug, nextPlayer);
                nextPlayer.PerformMove(_board);
            }
            WrapupGame();
            Debug(debug);
        }
        
        private void InitializeGame()
        {
            _board.Initialize();
            _stopwatch.Start();
        }

        private void WrapupGame()
        {
            _stopwatch.Stop();
            Console.WriteLine("Game ended..");
        }

        private void Debug(bool debug, IPlayer player = null)
        {
            if (!debug) return;

            VisualizeBoard();
            if (player != null)
                EchoNextPlayer(player);
            WaitForPlayerInput();
        }
        
        private void VisualizeBoard()
        {
            Console.WriteLine(_board.ToString());
        }

        private void EchoNextPlayer(IPlayer player)
        {
            var nextPlayer = player.Color.ToString();
            Console.WriteLine($"Next to move: {nextPlayer}");
        }

        private void WaitForPlayerInput()
        {
            Console.ReadLine();
        }
    }
}
