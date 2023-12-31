﻿

using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels.Reply;
using System.Collections.Generic;

namespace ForumSystemTeamFour.Models.ViewModels.Thread
{
    public class ThreadDetailsVM
    {
        public ThreadVM Thread { get; set; }        
        public ReplyQueryParametersVM QueryParameters { get; set; } 

        public ThreadDetailsVM()
        { 
            this.QueryParameters = new ReplyQueryParametersVM();
        }
    }
}
