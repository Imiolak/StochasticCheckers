using System.Linq;
using Checkers.Engine.Display;
using FluentAssertions;
using Xunit;

namespace Checkers.Engine.Tests.Display
{
    public class BoardTests
    {
        [Fact]
        public void NewlyInitializedBoardWithSize8ShouldHave12White12BlackPieces()
        {
            var board = new Board();
            board.Initialize();
            
            var whitePieces = board.GetPiecesForPlayer(PlayerColor.White);
            var blackPieces = board.GetPiecesForPlayer(PlayerColor.Black);

            board.EndGameConditionsMet.Should().BeFalse();
            whitePieces.Count().Should().Be(12);
            blackPieces.Count().Should().Be(12);
        }

        [Fact]
        public void WhitePlayerShouldBeOneToStart()
        {
            var board = new Board();

            board.NextPlayer.Should().Be(PlayerColor.White);
        }

        [Fact]
        public void NextPlayerShouldTakeActionAfterPreviousMoved()
        {
            const PlayerColor player = PlayerColor.Black;

            var board = new Board
            {
                LastPlayer = player,
                WasLastActionJump = false
            };

            board.NextPlayer.Should().Be(PlayerUtils.NextPlayer(player));
        }
    }
}
