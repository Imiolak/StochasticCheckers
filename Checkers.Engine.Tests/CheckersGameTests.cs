using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Checkers.Engine.Tests
{
    public class CheckersGameTests
    {
        [Fact]
        public void CreatingNewGameAssignsProperColoursToPlayers()
        {
            var player1 = Substitute.For<IPlayer>();
            var player2 = Substitute.For<IPlayer>();

            var game = new CheckersGame(player1, player2);

            player1.Color.Should().Be(PlayerColor.White);
            player2.Color.Should().Be(PlayerColor.Black);
        }
    }
}
