using System.Collections.Generic;
using System.Linq;
using Checkers.Engine.Actions;

namespace Checkers.Engine.Display
{
    public class Piece
    {
        public Piece(PlayerColor color, int row, int column)
        {
            Color = color;
            Row = row;
            Column = column;
        }

        public PlayerColor Color { get; }

        public int Row { get; set; }

        public int Column { get; set; }

        public IEnumerable<IAction> GetPossibleMoves(Board board)
        {
            var moves = new List<MoveAction>();

            var dRow = 1;
            var dColumn = 1;
            if (IsMovePossible(Row, Column, dRow, dColumn, board))
                moves.Add(new MoveAction(dRow, dColumn));

            dRow = 1;
            dColumn = -1;
            if (IsMovePossible(Row, Column, dRow, dColumn, board))
                moves.Add(new MoveAction(dRow, dColumn));

            dRow = -1;
            dColumn = -1;
            if (IsMovePossible(Row, Column, dRow, dColumn, board))
                moves.Add(new MoveAction(dRow, dColumn));

            dRow = -1;
            dColumn = 1;
            if (IsMovePossible(Row, Column, dRow, dColumn, board))
                moves.Add(new MoveAction(dRow, dColumn));

            return moves.AsEnumerable();
        }


        public IEnumerable<IAction> GetPossibleJumps(Board board)
        {
            var jumps = new List<JumpAction>();

            var dRow = 2;
            var dColumn = 2;
            var intRow = 1;
            var intColumn = 1;
            if (IsJumpPossible(Row, Column, dRow, dColumn, intRow, intColumn, board))
                jumps.Add(new JumpAction(dRow, dColumn));

            dRow = 2;
            dColumn = -2;
            intRow = 1;
            intColumn = -1;
            if (IsJumpPossible(Row, Column, dRow, dColumn, intRow, intColumn, board))
                jumps.Add(new JumpAction(dRow, dColumn));

            dRow = -2;
            dColumn = -2;
            intRow = -1;
            intColumn = -1;
            if (IsJumpPossible(Row, Column, dRow, dColumn, intRow, intColumn, board))
                jumps.Add(new JumpAction(dRow, dColumn));

            dRow = -2;
            dColumn = 2;
            intRow = -1;
            intColumn = 1;
            if (IsJumpPossible(Row, Column, dRow, dColumn, intRow, intColumn, board))
                jumps.Add(new JumpAction(dRow, dColumn));

            return jumps.AsEnumerable();
        }
        
        private bool IsMovePossible(int row, int column, int dRow, int dColumn, Board board)
        {
            return BoardHelper.CheckRowColumnConstraints(row + dRow, column + dColumn, board.BoardSize) && 
                board.Pieces[row + dRow][column + dColumn] == null;
        }

        private bool IsJumpPossible(int row, int column, int dRow, int dColumn, int intRow, int intColumn, Board board)
        {
            return BoardHelper.CheckRowColumnConstraints(row + dRow, column + dColumn, board.BoardSize) &&
                   BoardHelper.CheckRowColumnConstraints(row + intRow, column + intColumn, board.BoardSize) &&
                   board.Pieces[row + intRow][column + intColumn] != null &&
                   board.Pieces[row + intRow][column + intColumn].Color != Color &&
                   board.Pieces[row + dRow][column + dColumn] == null;
        }
    }
}