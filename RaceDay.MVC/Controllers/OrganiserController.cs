
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RaceDay.MVC.Controllers
{
    // Only users with the Organiser role can access this controller.
    [Authorize(Roles = "Organiser")]
    public class OrganiserController : Controller
    {
        // Displays the organiser dashboard.
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
