using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.Models;
using System.Security.Claims;

namespace RaceDay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfilesController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public ProfilesController(RaceDayContext context)
        {
            _context = context;
        }

        // Gets the profile belonging to the logged-in user.
        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdValue, out int userId))
                return Unauthorized();

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
                return NotFound("User profile not found.");

            if (user.Role == "Organiser")
            {
                var profile = await _context.OrganiserProfiles
                    .FirstOrDefaultAsync(p => p.UserId == userId);

                return profile == null ? NotFound("Organiser profile not found.") : Ok(profile);
            }

            var participantProfile = await _context.ParticipantProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);

            return participantProfile == null
                ? NotFound("Participant profile not found.")
                : Ok(participantProfile);
        }
    }
}