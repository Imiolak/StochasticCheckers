using System;

namespace Checkers.Engine.Exceptions
{
    public class ActionNotAllowedException : Exception
    {
        public ActionNotAllowedException(string message) : base(message)
        {
        }
    }
}
