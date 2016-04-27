using System;
using Checkers.Engine.Actions;
using Checkers.Engine.Display;
using Checkers.Engine.Exceptions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Checkers.Engine.Tests.Actions
{
    public class MoveActionTests
    {
        [Fact]
        public void PerformShouldMovePieceWhenDestinationCellIsNotOccupied()
        {
            const int row = 4;
            const int column = 4;
            const int deltaRow = 1;
            const int deltaColumn = 1;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);

            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var moveAction = new MoveAction(piece, deltaRow, deltaColumn);
            moveAction.Perform(board);

            piece.Row.Should().Be(row + deltaRow);
            piece.Column.Should().Be(column + deltaColumn);
            board.Pieces[row][column].Should().BeNull();
            board.Pieces[row + deltaRow][column + deltaColumn].Should().Be(piece);
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDestinationCellIsOccupied()
        {
            const int row = 4;
            const int column = 4;
            const int deltaRow = 1;
            const int deltaColumn = 1;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);
            var piece2 = Substitute.For<IPiece>();

            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;
            pieces[row + deltaRow][column + deltaColumn] = piece2;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var moveAction = new MoveAction(piece, deltaRow, deltaColumn);
            Action perform = () => moveAction.Perform(board);

            perform.ShouldThrow<DestinationCellOccupiedException>();
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDeltaRowIsNotOneOrMinusOne()
        {
            const int deltaRow = 2;
            const int deltaColumn = 1;

            var piece = Substitute.For<IPiece>();
            var board = Substitute.For<IBoard>();

            var moveAction = new MoveAction(piece, deltaRow, deltaColumn);
            Action perform = () => moveAction.Perform(board);

            perform.ShouldThrow<DeltaRowOutOfBoundsException>();
        }

        [Fact]
        public void PerformShouldThrowExceptionWhenDeltaColumnIsNotOneOrMinusOne()
        {
            const int deltaRow = 1;
            const int deltaColumn = 0;

            var piece = Substitute.For<IPiece>();
            var board = Substitute.For<IBoard>();

            var moveAction = new MoveAction(piece, deltaRow, deltaColumn);
            Action perform = () => moveAction.Perform(board);

            perform.ShouldThrow<DeltaColumnOutOfBoundsException>();
        }

        [Fact]
        public void UndoTest()
        {
            const int row = 4;
            const int column = 4;
            const int deltaRow = 1;
            const int deltaColumn = 1;

            var piece = Substitute.For<IPiece>();
            piece.Row.Returns(row);
            piece.Column.Returns(column);

            var pieces = new IPiece[8][];
            for (var i = 0; i < 8; i++)
                pieces[i] = new IPiece[8];
            pieces[row][column] = piece;

            var board = Substitute.For<IBoard>();
            board.Pieces.Returns(pieces);

            var moveAction = new MoveAction(piece, deltaRow, deltaColumn);
            moveAction.Undo(board);

            piece.Row.Should().Be(row - deltaRow);
            piece.Column.Should().Be(column - deltaColumn);
            board.Pieces[row][column].Should().BeNull();
            board.Pieces[row - deltaRow][column - deltaColumn].Should().Be(piece);
        }
    }
}
