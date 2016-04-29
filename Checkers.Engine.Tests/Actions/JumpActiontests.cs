using System;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Checkers.Engine.Tests.Actions
{
    public class JumpActionTests
    {
        [Fact]
        public void PerformShouldMovePieceAndRemoveIntermediateOneWhenDestinationCellIsNotOccupied()
        {
            const int row = 4;
            const int column = 4;
            const int intRow = row + 1;
            const int intColumn = column + 1;
            const int deltaRow = 2;
            const int deltaColumn = 2;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);
            piece.Color.Returns(PlayerColor.White);

            var beatenPiece = Substitute.For<IPiece>();
            beatenPiece.Color.Returns(PlayerColor.Black);

            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;
            pieces[intRow][intColumn] = beatenPiece;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var jumpAction = new JumpAction(piece, deltaRow, deltaColumn);
            jumpAction.Perform(board);

            piece.Row.Should().Be(row + deltaRow);
            piece.Column.Should().Be(column + deltaColumn);
            board.Pieces[intRow][intColumn].Should().BeNull();
            board.Pieces[row][column].Should().BeNull();
            board.Pieces[row + deltaRow][column + deltaColumn].Should().Be(piece);
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDestinationCellIsOccupied()
        {
            const int row = 4;
            const int column = 4;
            const int intRow = row + 1;
            const int intColumn = column + 1;
            const int deltaRow = 2;
            const int deltaColumn = 2;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);
            piece.Color.Returns(PlayerColor.White);

            var beatenPiece = Substitute.For<IPiece>();
            beatenPiece.Color.Returns(PlayerColor.Black);

            var piece2 = Substitute.For<IPiece>();
            
            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;
            pieces[intRow][intColumn] = beatenPiece;
            pieces[row + deltaRow][column + deltaColumn] = piece2;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var jumpAction = new JumpAction(piece, deltaRow, deltaColumn);
            Action perform = () => jumpAction.Perform(board);

            perform.ShouldThrow<DestinationCellOccupiedException>();
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDeltaRowIsNotTwoOrMinusTwo()
        {
            const int deltaRow = 1;
            const int deltaColumn = 2;

            var piece = Substitute.For<IPiece>();
            var board = Substitute.For<IBoard>();

            var jumpAction = new JumpAction(piece, deltaRow, deltaColumn);
            Action perform = () => jumpAction.Perform(board);

            perform.ShouldThrow<DeltaRowOutOfBoundsException>();
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDeltaColumnIsNotTwoOrMinusTwo()
        {
            const int deltaRow = 2;
            const int deltaColumn = 5;

            var piece = Substitute.For<IPiece>();
            var board = Substitute.For<IBoard>();

            var jumpAction = new JumpAction(piece, deltaRow, deltaColumn);
            Action perform = () => jumpAction.Perform(board);

            perform.ShouldThrow<DeltaColumnOutOfBoundsException>();
        }

        [Fact]
        public void PerformShouldSetLastPlayerAndWasJumpOnBoard()
        {
            const int row = 4;
            const int column = 4;
            const int intRow = row + 1;
            const int intColumn = column + 1;
            const int deltaRow = 2;
            const int deltaColumn = 2;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);
            piece.Color.Returns(PlayerColor.Black);

            var beatenPiece = Substitute.For<IPiece>();
            beatenPiece.Color.Returns(PlayerColor.White);

            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;
            pieces[intRow][intColumn] = beatenPiece;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var jumpAction = new JumpAction(piece, deltaRow, deltaColumn);
            jumpAction.Perform(board);

            board.LastPlayer.Should().Be(piece.Color);
            board.WasLastActionJump.Should().BeTrue();
        }
    }
}
