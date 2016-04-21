using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkers.Engine.Display
{
    public class Board
    {
        public Board()
        {
            InitializeBoard();
        }

        public int BoardSize => 8;

        public Piece[][] Pieces { get; private set; }

        public bool EndGameConditionsMet => !GetPiecesForPlayer(PlayerColor.Black).Any() || 
            !GetPiecesForPlayer(PlayerColor.White).Any();
        
        public IEnumerable<Piece> GetPiecesForPlayer(PlayerColor playerColor)
        {
            var pieces = new List<Piece>();
            
            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    if (Pieces[i][j] != null && Pieces[i][j].Color == playerColor)
                        pieces.Add(Pieces[i][j]);
                }
            }

            return pieces.AsEnumerable();
        }

        public IEnumerable<Piece> GetJumpablePiecesForPlayer(PlayerColor color)
        {
            return GetPiecesForPlayer(color).Where(CanPerformJump);
        }
        
        public IEnumerable<Piece> GetMovablePiecesForPlayer(PlayerColor color)
        {
            return GetPiecesForPlayer(color).Where(CanPerformMove);
        }

        public override string ToString()
        {
            return GetBoardStringRepresentation();
        }
        
        private bool CanPerformJump(Piece piece)
        {
            var otherPieces = OtherPlayersPiecesNear(piece);
            return otherPieces.Any(o => CanJumpOver(piece, o));
        }
        
        private IEnumerable<Piece> OtherPlayersPiecesNear(Piece piece)
        {
            var row = piece.Row;
            var column = piece.Column;
            var color = piece.Color;

            var otherPieces = new List<Piece>();

            var pRow = row + 1;
            var pColumn = column + 1;
            if (IsOtherPieceNear(pRow, pColumn, color))
                otherPieces.Add(Pieces[pRow][pColumn]);

            pRow = row + 1;
            pColumn = column - 1;
            if (IsOtherPieceNear(pRow, pColumn, color))
                otherPieces.Add(Pieces[pRow][pColumn]);

            pRow = row - 1;
            pColumn = column - 1;
            if (IsOtherPieceNear(pRow, pColumn, color))
                otherPieces.Add(Pieces[pRow][pColumn]);

            pRow = row - 1;
            pColumn = column + 1;
            if (IsOtherPieceNear(pRow, pColumn, color))
                otherPieces.Add(Pieces[pRow][pColumn]);

            return otherPieces.AsEnumerable();
        }

        private bool IsOtherPieceNear(int pRow, int pColumn, PlayerColor color)
        {
            return BoardHelper.CheckRowColumnConstraints(pRow, pColumn, BoardSize) &&
                Pieces[pRow][pColumn] != null &&
                Pieces[pRow][pColumn].Color != color;
        }

        private bool CanJumpOver(Piece piece, Piece other)
        {
            var row = (other.Row - piece.Row)*2 + piece.Row;
            var column = (other.Column - piece.Column)*2 + piece.Column;
            
            return BoardHelper.CheckRowColumnConstraints(row, column, BoardSize) && Pieces[row][column] == null;
        }

        private bool CanPerformMove(Piece piece)
        {
            var row = piece.Row;
            var column = piece.Column;

            var pRow = row + 1;
            var pColumn = column + 1;
            if (IsMovable(pRow, pColumn))
                return true;

            pRow = row + 1;
            pColumn = column - 1;
            if (IsMovable(pRow, pColumn))
                return true;

            pRow = row - 1;
            pColumn = column - 1;
            if (IsMovable(pRow, pColumn))
                return true;

            pRow = row - 1;
            pColumn = column + 1;
            if (IsMovable(pRow, pColumn))
                return true;

            return false;
        }

        private bool IsMovable(int pRow, int pColumn)
        {
            return BoardHelper.CheckRowColumnConstraints(pRow, pColumn, BoardSize) && Pieces[pRow][pColumn] == null;
        }
        
        private void InitializeBoard()
        {
            Pieces = new Piece[BoardSize][];
            for (var i = 0; i < BoardSize; i++) { 
                Pieces[i] = new Piece[BoardSize];
            }

            Pieces[0][0] = new Piece(PlayerColor.White, 0, 0);
            Pieces[0][2] = new Piece(PlayerColor.White, 0, 2);
            Pieces[0][4] = new Piece(PlayerColor.White, 0, 4);
            Pieces[0][6] = new Piece(PlayerColor.White, 0, 6);
            Pieces[1][1] = new Piece(PlayerColor.White, 1, 1);
            Pieces[1][3] = new Piece(PlayerColor.White, 1, 3);
            Pieces[1][5] = new Piece(PlayerColor.White, 1, 5);
            Pieces[1][7] = new Piece(PlayerColor.White, 1, 7);
            Pieces[2][0] = new Piece(PlayerColor.White, 2, 0);
            Pieces[2][2] = new Piece(PlayerColor.White, 2, 2);
            Pieces[2][4] = new Piece(PlayerColor.White, 2, 4);
            Pieces[2][6] = new Piece(PlayerColor.White, 2, 6);
            Pieces[7][1] = new Piece(PlayerColor.Black, 7, 1);
            Pieces[7][3] = new Piece(PlayerColor.Black, 7, 3);
            Pieces[7][5] = new Piece(PlayerColor.Black, 7, 5);
            Pieces[7][7] = new Piece(PlayerColor.Black, 7, 7);
            Pieces[6][0] = new Piece(PlayerColor.Black, 6, 0);
            Pieces[6][2] = new Piece(PlayerColor.Black, 6, 2);
            Pieces[6][4] = new Piece(PlayerColor.Black, 6, 4);
            Pieces[6][6] = new Piece(PlayerColor.Black, 6, 6);
            Pieces[5][1] = new Piece(PlayerColor.Black, 5, 1);
            Pieces[5][3] = new Piece(PlayerColor.Black, 5, 3);
            Pieces[5][5] = new Piece(PlayerColor.Black, 5, 5);
            Pieces[5][7] = new Piece(PlayerColor.Black, 5, 7);
        }

        private string GetBoardStringRepresentation()
        {
            var sb = new StringBuilder();
            for (var row = BoardSize - 1; row >= 0; row--)
            {
                if (row == BoardSize - 1)
                    sb.AppendLine("---|---|---|---|---|---|---|---");
                for (var column = 0; column < BoardSize; column++)
                {
                    string rep;
                    var piece = Pieces[row][column];
                    if (piece == null)
                        rep = "   ";
                    else
                        rep = Pieces[row][column].Color == PlayerColor.White ? " W " : " B ";
                    sb.Append(rep);
                    if (column < BoardSize - 1)
                        sb.Append("|");
                }
                sb.AppendLine();
                sb.AppendLine("---|---|---|---|---|---|---|---");
            }
            return sb.ToString();
        }
    }
}