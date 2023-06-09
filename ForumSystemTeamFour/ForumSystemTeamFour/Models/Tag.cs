using ForumSystemTeamFour.Models.Interfaces;

namespace ForumSystemTeamFour.Models
{
    public class Tag : ITag
    {
        public int Id { get ; set ; }
        public string Name { get; set; }
    }
}
