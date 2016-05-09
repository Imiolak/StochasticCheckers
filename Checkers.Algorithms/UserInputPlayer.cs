using System;
using Checkers.Engine;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;

namespace Checkers.Algorithms
{
    public class UserInputPlayer : IPlayer
    {
        public PlayerColor Color { get; set; }
        public void PerformMove(IBoard board)
        {
            IAction action = null;
            bool validInput;

            Console.WriteLine("Your move, please provide input:");
            do
            {
                try
                {
                    var input = ReadInput();
                    validInput = ParseInput(input, board, out action);
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong, try agagin.");
                    validInput = false;
                }
                
            } while (!validInput);
            
            action.Perform(board);
        }
        
        private string ReadInput()
        {
            Console.WriteLine("Format: AX BY (AX - piece location) (BY - destination)");

            return Console.ReadLine();
        }

        private bool ParseInput(string input, IBoard board, out IAction action)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Wrong format, try agagin.");
                action = null;
                return false;
            }

            var split = input.Split(' ');
            var pieceLocation = Boardify(split[0]);
            var destination = Boardify(split[1]);

            var piece = board.Pieces[pieceLocation[0]][pieceLocation[1]];
            if (piece == null || piece.Color != Color)
            {
                Console.WriteLine("Chose propec cell.");
                action = null;
                return false;
            }

            action = ActionFactory.Create(piece, destination[0], destination[1]);
            return true;
        }

        private int[] Boardify(string userProvidedLocation)
        {
            var location = new int[2];

            location[0] = userProvidedLocation[0] - 'A';
            location[1] = userProvidedLocation[1] - '1';

            return location;
        }
    }
}
