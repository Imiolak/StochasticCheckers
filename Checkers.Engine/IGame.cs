using System;
using Checkers.Engine.Display;

namespace Checkers.Engine
{
    public interface IGame
    {
        GameResult Result { get; }
        TimeSpan TimeElapsed { get; }

        void Start(bool debug = false);
    }
}