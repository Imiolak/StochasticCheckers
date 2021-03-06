﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Checkers.Engine.Actions;

namespace Checkers.Engine.Display
{
    public class Board : IBoard
    {
        private bool _gameStarted = false;

        private List<IAction> _previousPlayersActions;
        private IAction _lastAction;

        public int BoardSize => 8;

        public bool EndGameConditionsMet
        {
            get
            {
                if (!GetValidActionsForPlayer(PlayerColor.Black).Any())
                {
                    GameResult = GameResult.WhiteWon;
                    return true;
                }
                if (!GetValidActionsForPlayer(PlayerColor.White).Any())
                {
                    GameResult = GameResult.BlackWon;
                    return true;
                }
                return false;
            }
        }

        public GameResult GameResult { get; private set; }
        
        public IPiece[][] Pieces { get; private set; }

        public PlayerColor LastPlayer { get; set; }

        public PlayerColor NextPlayer
        {
            get
            {
                if (!_gameStarted)
                {
                    _gameStarted = true;
                    return PlayerUtils.StartingPlayer();
                }
                if (WasLastActionJump && LastAction.Piece.GetPossibleJumps(this).Any())
                {
                    return LastPlayer;
                }
                return PlayerUtils.NextPlayer(LastPlayer);
            }
        }
        
        public IAction LastAction
        {
            get { return _lastAction; }
            set
            {
                if (_previousPlayersActions.Any() && _previousPlayersActions.First().Piece.Color != value.Piece.Color)
                    _previousPlayersActions.Clear();

                _previousPlayersActions.Add(value);
                _lastAction = value;
            }
        }

        public IEnumerable<IAction> PreviousPlayersActions => _previousPlayersActions.AsEnumerable();

        public bool WasLastActionJump { get; set; }
        
        public void Initialize()
        {
            Pieces = InitializeBoard();
            _previousPlayersActions = new List<IAction>();
        }

        public IEnumerable<IPiece> GetPiecesForPlayer(PlayerColor playerColor)
        {
            var pieces = new List<IPiece>();
            
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

        public IEnumerable<IAction> GetValidActionsForPlayer(PlayerColor playerColor)
        {
            if (WasLastActionJump)
            {
                var nextPossibleJumps = LastAction.Piece.GetPossibleJumps(this).ToArray();
                if (nextPossibleJumps.Any())
                    return nextPossibleJumps;
            }

            var pieces = GetJumpablePiecesForPlayer(playerColor);

            var pieceArray = pieces as Piece[] ?? pieces.ToArray();
            if (pieceArray.Any())
                return pieceArray.SelectMany(piece => piece.GetPossibleJumps(this));

            pieces = GetMovablePiecesForPlayer(playerColor);

            pieceArray = pieces as Piece[] ?? pieces.ToArray();
            if (pieceArray.Any())
            {
                return pieceArray.SelectMany(piece => piece.GetPossibleMoves(this));
            }

            return Enumerable.Empty<IAction>();
        }

        public override string ToString()
        {
            return GetBoardStringRepresentation();
        }

        #region Private Methods
        private IEnumerable<IPiece> GetJumpablePiecesForPlayer(PlayerColor color)
        {
            return GetPiecesForPlayer(color).Where(CanPerformJump);
        }

        private IEnumerable<IPiece> GetMovablePiecesForPlayer(PlayerColor color)
        {
            return GetPiecesForPlayer(color).Where(CanPerformMove);
        }

        
        private bool CanPerformJump(IPiece piece)
        {
            var otherPieces = OtherPlayersPiecesNear(piece);
            return otherPieces.Any(o => CanJumpOver(piece, o));
        }
        
        private IEnumerable<IPiece> OtherPlayersPiecesNear(IPiece piece)
        {
            var row = piece.Row;
            var column = piece.Column;
            var color = piece.Color;

            var otherPieces = new List<IPiece>();

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

        private bool CanJumpOver(IPiece piece, IPiece other)
        {
            var row = (other.Row - piece.Row)*2 + piece.Row;
            var column = (other.Column - piece.Column)*2 + piece.Column;
            
            return BoardHelper.CheckRowColumnConstraints(row, column, BoardSize) && Pieces[row][column] == null;
        }

        private bool CanPerformMove(IPiece piece)
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
        
        private IPiece[][] InitializeBoard()
        {
            var pieces = new IPiece[BoardSize][];
            for (var i = 0; i < BoardSize; i++) {
                pieces[i] = new IPiece[BoardSize];
            }

            pieces[0][0] = new Piece(PlayerColor.White, 0, 0);
            pieces[0][2] = new Piece(PlayerColor.White, 0, 2);
            pieces[0][4] = new Piece(PlayerColor.White, 0, 4);
            pieces[0][6] = new Piece(PlayerColor.White, 0, 6);
            pieces[1][1] = new Piece(PlayerColor.White, 1, 1);
            pieces[1][3] = new Piece(PlayerColor.White, 1, 3);
            pieces[1][5] = new Piece(PlayerColor.White, 1, 5);
            pieces[1][7] = new Piece(PlayerColor.White, 1, 7);
            pieces[2][0] = new Piece(PlayerColor.White, 2, 0);
            pieces[2][2] = new Piece(PlayerColor.White, 2, 2);
            pieces[2][4] = new Piece(PlayerColor.White, 2, 4);
            pieces[2][6] = new Piece(PlayerColor.White, 2, 6);
            pieces[7][1] = new Piece(PlayerColor.Black, 7, 1);
            pieces[7][3] = new Piece(PlayerColor.Black, 7, 3);
            pieces[7][5] = new Piece(PlayerColor.Black, 7, 5);
            pieces[7][7] = new Piece(PlayerColor.Black, 7, 7);
            pieces[6][0] = new Piece(PlayerColor.Black, 6, 0);
            pieces[6][2] = new Piece(PlayerColor.Black, 6, 2);
            pieces[6][4] = new Piece(PlayerColor.Black, 6, 4);
            pieces[6][6] = new Piece(PlayerColor.Black, 6, 6);
            pieces[5][1] = new Piece(PlayerColor.Black, 5, 1);
            pieces[5][3] = new Piece(PlayerColor.Black, 5, 3);
            pieces[5][5] = new Piece(PlayerColor.Black, 5, 5);
            pieces[5][7] = new Piece(PlayerColor.Black, 5, 7);

            return pieces;
        }

        private string GetBoardStringRepresentation()
        {
            var rowLetter = 'H';

            var sb = new StringBuilder();
            for (var row = BoardSize - 1; row >= 0; row--)
            {
                if (row == BoardSize - 1)
                    sb.AppendLine("   |---|---|---|---|---|---|---|---|");
                sb.Append($" {rowLetter--} |"); 
                for (var column = 0; column < BoardSize; column++)
                {
                    string rep;
                    var piece = Pieces[row][column];
                    if (piece == null)
                        rep = "   ";
                    else
                        rep = Pieces[row][column].Color == PlayerColor.White ? " W " : " B ";
                    sb.Append(rep);
                    sb.Append("|");
                }
                sb.AppendLine();
                sb.AppendLine("   |---|---|---|---|---|---|---|---|");
            }
            sb.Append("    ");
            for (var i = 1; i <= 8; i++)
                sb.Append(string.Format($" {i}  "));
            return sb.ToString();
        }
        #endregion
    }
}