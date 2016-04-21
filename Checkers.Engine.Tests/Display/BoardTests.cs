using System.Linq;
using Checkers.Engine.Display;
using FluentAssertions;
using Xunit;

namespace Checkers.Engine.Tests.Display
{
    public class BoardTests
    {
        [Fact]
        public void NewlyCreatedBoardWithSize8ShouldHave24Pieces12White12Black()
        {
            var board = new Board();

            var pieces = board.GetAllPieces();
            var whitePieces = board.GetPiecesForPlayer(PlayerColor.White);
            var blackPieces = board.GetPiecesForPlayer(PlayerColor.Black);

            board.EndGameConditionsMet.Should().BeFalse();
            pieces.Count().Should().Be(24);
            whitePieces.Count().Should().Be(12);
            blackPieces.Count().Should().Be(12);
        }
    }
}
