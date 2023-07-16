using ForumSystemTeamFour.Mappers.Interfaces;
using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using ForumSystemTeamFour.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ForumSystemTeamFour.Models.ViewModels
{
	public class AnonymousHomeVM
	{
		

		public int NumberOfThreads { get; set; }
		public int TotalUsers { get; set; }
        public List<string> UsersOnline { get; set; }
        public int NumberOfPosts { get; set; }
        public List<string> TopTags { get; set; }
		public List<ThreadVM> TopThreads { get; set; }
    }
}
