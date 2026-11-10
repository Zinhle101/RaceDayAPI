
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    // Only users with the Participant role can access this controller.
    [Authorize(Roles = "Participant")]
    public class ParticipantController : Controller
    {
        // Displays the participant dashboard.
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
