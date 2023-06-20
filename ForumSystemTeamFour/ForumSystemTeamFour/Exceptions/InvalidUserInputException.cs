using System;

namespace ForumSystemTeamFour.Exceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException() : base()
        { }
        public InvalidUserInputException(string errorMessage) : base(errorMessage)
        { }
    }
}
