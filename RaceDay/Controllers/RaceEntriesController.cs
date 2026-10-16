
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.Models;

namespace RaceDay.API.Controllers
{
    // Handles requests related to participant race entries.
    [ApiController]
    [Route("api/[controller]")]
    public class RaceEntriesController : ControllerBase
    {
        private readonly RaceDayContext _context;

        // Connects the controller to the database context.
        public RaceEntriesController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/RaceEntries
        // Retrieves all race entries from the database.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceEntry>>> GetRaceEntries()
        {
            return await _context.RaceEntries.ToListAsync();
        }

        // GET: api/RaceEntries/5
        // Retrieves one race entry using its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceEntry>> GetRaceEntry(int id)
        {
            // Finds the race entry in the database.
            var raceEntry = await _context.RaceEntries.FindAsync(id);

            // Checks whether the race entry exists.
            if (raceEntry == null)
            {
                return NotFound();
            }

            // Returns the race entry if it exists.
            return raceEntry;
        }

        // POST: api/RaceEntries
        // Creates a new race entry for a participant.
        [HttpPost]
        public async Task<ActionResult<RaceEntry>> CreateRaceEntry(RaceEntry raceEntry)
        {
            // Checks whether the participant exists.
            var participantExists = await _context.Users
                .AnyAsync(u => u.UserID == raceEntry.UserId);

            if (!participantExists)
            {
                return BadRequest("The selected participant does not exist.");
            }

            // Checks whether the selected race category exists.
            var categoryExists = await _context.RaceCategories
                .AnyAsync(c => c.RaceCategoryId == raceEntry.RaceCategoryId);

            if (!categoryExists)
            {
                return BadRequest("The selected race category does not exist.");
            }

            // Adds the race entry to the database context.
            _context.RaceEntries.Add(raceEntry);

            // Saves the new race entry to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the entry was created.
            return CreatedAtAction(
                nameof(GetRaceEntry),
                new { id = raceEntry.RaceEntryId },
                raceEntry);
        }

    }
}
