using System;

namespace ForumSystemTeamFour.Exceptions
{
    public class DuplicateEntityException : ApplicationException
    {
        public DuplicateEntityException() : base()
        { }
        public DuplicateEntityException(string errorMessage) : base(errorMessage)
        { }
    }
}
