

using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels.Reply;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.ViewModels.Thread
{
    public class ThreadDetailsVM
    {
        public LargeThreadResponseDto Thread { get; set; } //Change to VM later?       
        public ReplyQueryParametersVM QueryParameters { get; set; } 

        public ThreadDetailsVM()
        { 
            this.QueryParameters = new ReplyQueryParametersVM();
        }
    }
}
