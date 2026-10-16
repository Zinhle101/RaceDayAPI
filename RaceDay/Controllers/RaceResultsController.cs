
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.Models;

namespace RaceDay.API.Controllers
{
    // Handles requests related to race results.
    [ApiController]
    [Route("api/[controller]")]
    public class RaceResultsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        // Connects the controller to the database context.
        public RaceResultsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/RaceResults
        // Retrieves all race results from the database.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceResult>>> GetRaceResults()
        {
            return await _context.RaceResults.ToListAsync();
        }

        // GET: api/RaceResults/5
        // Retrieves one race result using its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceResult>> GetRaceResult(int id)
        {
            // Finds the race result in the database.
            var raceResult = await _context.RaceResults.FindAsync(id);

            // Checks whether the race result exists.
            if (raceResult == null)
            {
                return NotFound();
            }

            // Returns the result if it exists.
            return raceResult;
        }
    }
}
