using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.Interfaces
{
    public interface ITag
    {
        int Id { get; set; }
        string Name { get; set; }
        List<Thread> Threads { get; set; }
    }
}
