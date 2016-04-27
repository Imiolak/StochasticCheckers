using System;

namespace Checkers.Engine.Exceptions
{
    public class DeltaRowOutOfBoundsException : Exception
    {
        public DeltaRowOutOfBoundsException(string message) : base(message) { }
    }
}
