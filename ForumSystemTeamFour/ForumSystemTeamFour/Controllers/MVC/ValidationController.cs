using Microsoft.AspNetCore.Mvc;

namespace ForumSystemTeamFour.Controllers.MVC
{
    public class ValidationController : Controller
    {
        [HttpPost]
        public IActionResult CheckEmptyString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return Json($"This field cannot be empty!");
            }

            return Json(true);
        }
    }
}
