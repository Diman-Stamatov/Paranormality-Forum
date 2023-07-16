using ForumSystemTeamFour.Models.DTOs.ThreadDTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ForumSystemTeamFour.Models.ViewModels.Thread
{
    public class ThreadCreateVM : ThreadCreateDto
    {
        public SelectList TagsList  { get; set; }
        public string SelectedTag { get; set; }
    }
}
