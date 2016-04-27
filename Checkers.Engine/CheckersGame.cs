using System;
using System.Text;
using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public class CheckersGame
    {
        private readonly IPlayer _player1;
        private readonly IPlayer _player2;

        private readonly Board _board;

        public CheckersGame(IPlayer player1, IPlayer player2)
        {
            _player1 = player1;
            _player2 = player2;

            _player1.Color = PlayerColor.White;
            _player2.Color = PlayerColor.Black;

            _board = new Board();
        }

        public void Start(bool debug = false)
        {
            _board.Initialize();

            while (true)
            {
                if (_board.EndGameConditionsMet)
                    break;
                DoPlayerMove(_player1, debug);
                if (_board.EndGameConditionsMet)
                    break;
                DoPlayerMove(_player2, debug);
            }
            VisualizeBoard(_board);
            WaitForPlayerInput();
        }

        private void DoPlayerMove(IPlayer player, bool debug)
        {
            bool didJump;
            do
            {
                if (debug)
                {
                    VisualizeBoard(_board);
                    EchoNextPlayer(player);
                    WaitForPlayerInput();
                }
                player.PerformMove(_board, out didJump);
            } while (didJump);
        }
        
        private void VisualizeBoard(Board board)
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
            Console.ReadKey();
        }
    }
}
