
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

        // POST: api/RaceResults
        // Creates a new result for a race entry.
        [HttpPost]
        public async Task<ActionResult<RaceResult>> CreateRaceResult(RaceResult raceResult)
        {
            // Checks whether the race entry exists.
            var raceEntryExists = await _context.RaceEntries
                .AnyAsync(e => e.RaceEntryId == raceResult.RaceEntryId);

            if (!raceEntryExists)
            {
                return BadRequest("The selected race entry does not exist.");
            }

            // Adds the new result to the database context.
            _context.RaceResults.Add(raceResult);

            // Saves the result to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the result was created.
            return CreatedAtAction(
                nameof(GetRaceResult),
                new { id = raceResult.RaceResultId },
                raceResult);
        }

        // PUT: api/RaceResults/5
        // Updates an existing race result.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRaceResult(int id, RaceResult raceResult)
        {
            // Checks whether the URL ID matches the result ID.
            if (id != raceResult.RaceResultId)
            {
                return BadRequest();
            }

            // Finds the existing result in the database.
            var existingResult = await _context.RaceResults.FindAsync(id);

            // Checks whether the result exists.
            if (existingResult == null)
            {
                return NotFound();
            }

            // Checks whether the race entry exists.
            var raceEntryExists = await _context.RaceEntries
                .AnyAsync(e => e.RaceEntryId == raceResult.RaceEntryId);

            if (!raceEntryExists)
            {
                return BadRequest("The selected race entry does not exist.");
            }

            // Updates the result details.
            existingResult.RaceEntryId = raceResult.RaceEntryId;
            existingResult.FinishingPosition = raceResult.FinishingPosition;
            existingResult.FinishTime = raceResult.FinishTime;

            // Saves the changes to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming the update.
            return NoContent();
        }

        // DELETE: api/RaceResults/5
        // Deletes an existing race result.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaceResult(int id)
        {
            // Finds the result using its ID.
            var raceResult = await _context.RaceResults.FindAsync(id);

            // Checks whether the result exists.
            if (raceResult == null)
            {
                return NotFound();
            }

            // Removes the result from the database context.
            _context.RaceResults.Remove(raceResult);

            // Saves the deletion to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the deletion was successful.
            return NoContent();
        }

    }
}
