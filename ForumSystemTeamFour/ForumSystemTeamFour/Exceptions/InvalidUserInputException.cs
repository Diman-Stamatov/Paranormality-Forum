namespace ForumSystemTeamFour.Exceptions
{
    public class InvalidUserInputException : ApplicationException
    {
        public InvalidUserInputException(string errorMessage) : base(errorMessage)
        { }
    }
}
