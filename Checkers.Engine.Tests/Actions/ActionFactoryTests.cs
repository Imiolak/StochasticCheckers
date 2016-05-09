using Checkers.Engine.Actions;
using Checkers.Engine.Display;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Checkers.Engine.Tests.Actions
{
    public class ActionFactoryTests
    {
        [Fact]
        public void FactoryShouldCreateMoveActionWhenGivenOneDeltas()
        {
            var piece = Substitute.For<IPiece>();
            const int dRow = 1;
            const int dColumn = -1;

            var action = ActionFactory.Create(piece, dRow, dColumn);

            action.Should().BeOfType(typeof(MoveAction));
        }

        [Fact]
        public void FactoryShouldCreateJumpActionsWhenGivenTwoDeltas()
        {
            var piece = Substitute.For<IPiece>();
            const int dRow = -2;
            const int dColumn = 2;

            var action = ActionFactory.Create(piece, dRow, dColumn);

            action.Should().BeOfType(typeof(JumpAction));
        }
    }
}
