using System;
using Checkers.Engine;
using Checkers.Engine.Display;

namespace Checkers.Algorithms
{
    public class MTCSPlayer : IPlayer
    {
        public PlayerColor Color { get; set; }

        public void PerformMove(Board board, out bool wasJump)
        {
            throw new NotImplementedException();
        }
    }
}
