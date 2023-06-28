using System;

namespace ForumSystemTeamFour.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
    {
        public UnauthorizedOperationException(string message)
            : base(message)
        {
        }
    }
}
