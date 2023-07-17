

using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Models.QueryParameters;
using ForumSystemTeamFour.Models.ViewModels.Reply;
using System.Collections.Generic;



namespace ForumSystemTeamFour.Models.ViewModels.Thread
{
	public class ThreadIndexVM
	{
        public string SelectedTag { get; set; }
        public List<ShortThreadResponseDto> Threads { get; set; }
		public ThreadQueryParameters QueryParameters { get; set; }

		public ThreadIndexVM()
		{
			this.QueryParameters = new ThreadQueryParameters();
		}
	}
}
