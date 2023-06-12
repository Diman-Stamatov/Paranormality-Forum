namespace ForumSystemTeamFour.Exceptions
{
    public class EntityNotFoundException :ApplicationException
    {
        public EntityNotFoundException(string errorMessage) : base(errorMessage)
        { }
    }
}
