using Checkers.Engine.Display;
using FluentAssertions;
using Xunit;

namespace Checkers.Engine.Tests.Display
{
    public class PieceTests
    {
        [Fact]
        public void PiecesWithSameParamsShouldBeEqual()
        {
            var piece = new Piece(PlayerColor.White, 5, 5);
            var piece2 = new Piece(PlayerColor.White, 5, 5);

            piece.Should().Be(piece2);
        }

        [Fact]
        public void PiecesWithDifferentRowOrColumnShouldNotBeEqual()
        {
            var piece = new Piece(PlayerColor.White, 5, 5);
            var piece2 = new Piece(PlayerColor.White, 5, 6);

            piece.Should().NotBe(piece2);
        }

        [Fact]
        public void PiecesWithDifferentColorShouldNotBeEqual()
        {
            var piece = new Piece(PlayerColor.White, 5, 5);
            var piece2 = new Piece(PlayerColor.Black, 5, 5);

            piece.Should().NotBe(piece2);
        }
    }
}
