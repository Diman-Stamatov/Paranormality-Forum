using Microsoft.AspNetCore.Http;

namespace ForumSystemTeamFour.Models.ViewModels
{
    public class ProfilePicVM
    {
        public string FileName { set; get; }        
        public IFormFile ProfilePicture { set; get; }
    }
}
