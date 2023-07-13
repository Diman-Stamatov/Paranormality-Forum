

using ForumSystemTeamFour.Models.QueryParameters;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class ThreadWithRepliesVM
    {
        public Thread Thread { get; set; } //Change to VM later       
        public ReplyQueryParameters QueryParameters { get; set; } //Change to VM later?
    }
}
