using System;

namespace Checkers.Engine.Exceptions
{
    public class DeltaColumnOutOfBoundsException : Exception
    {
        public DeltaColumnOutOfBoundsException(string message) : base(message) { }
    }
}
