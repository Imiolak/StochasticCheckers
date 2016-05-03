using System;
using System.Collections.Generic;
using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public class CheckersGame
    {
        private readonly IDictionary<PlayerColor, IPlayer> _players;
        private readonly Board _board;

        private bool _gameEnded;

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
        }

        public PlayerColor Winner => _board.LastPlayer;

        public void Start(bool debug = false)
        {
            _board.Initialize();

            while (!_board.EndGameConditionsMet)
            {
                var nextPlayer = _players[_board.NextPlayer];
                Debug(debug, nextPlayer);
                nextPlayer.PerformMove(_board);
            }
            _gameEnded = true;
            Debug(debug);
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
