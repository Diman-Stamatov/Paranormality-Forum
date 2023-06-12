namespace ForumSystemTeamFour.Exceptions
{
    public class DuplicateEntityException : ApplicationException
    {
        public DuplicateEntityException(string errorMessage) : base(errorMessage)
        { }
    }
}
