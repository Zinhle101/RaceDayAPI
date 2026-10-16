using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;
using RaceDay.Models;
using Microsoft.AspNetCore.Authorization;

namespace RaceDay.API.Controllers
{
    // Handles requests related to race categories.
    [ApiController]
    [Route("api/[controller]")]
    public class RaceCategoriesController : ControllerBase
    {
        private readonly RaceDayContext _context;

        // Connects the controller to the database context.
        public RaceCategoriesController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/RaceCategories
        // Retrieves all race categories from the database.
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceCategory>>> GetRaceCategories()
        {
            return await _context.RaceCategories.ToListAsync();
        }

        // GET: api/RaceCategories/ByEvent/1
        // Retrieves all categories belonging to a specific race event.
        [HttpGet("ByEvent/{raceEventId}")]
        public async Task<ActionResult<IEnumerable<RaceCategory>>> GetCategoriesByEvent(int raceEventId)
        {
            // Checks whether the race event exists.
            var raceEventExists = await _context.RaceEvents
                .AnyAsync(e => e.RaceEventId == raceEventId);

            if (!raceEventExists)
            {
                return NotFound("Race event not found.");
            }

            // Retrieves categories linked to the selected event.
            var categories = await _context.RaceCategories
                .Where(c => c.RaceEventId == raceEventId)
                .ToListAsync();

            return categories;
        }

        // POST: api/RaceCategories
        // Creates a new category for a race event.

        [Authorize(Roles = "Organiser")]
        [HttpPost]
        public async Task<ActionResult<RaceCategory>> CreateRaceCategory(RaceCategory raceCategory)
        {
            // Checks whether the race event exists.
            var raceEventExists = await _context.RaceEvents
                .AnyAsync(e => e.RaceEventId == raceCategory.RaceEventId);

            if (!raceEventExists)
            {
                return BadRequest("The selected race event does not exist.");
            }

            // Adds the new category to the database context.
            _context.RaceCategories.Add(raceCategory);

            // Saves the new category to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the category was created.
            return CreatedAtAction(
                nameof(GetRaceCategories),
                new { id = raceCategory.RaceCategoryId },
                raceCategory);
        }

        // GET: api/RaceCategories/5
        // Retrieves one race category using its ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceCategory>> GetRaceCategory(int id)
        {
            // Finds the category in the database.
            var raceCategory = await _context.RaceCategories.FindAsync(id);

            // Checks whether the category exists.
            if (raceCategory == null)
            {
                return NotFound();
            }

            // Returns the category if it exists.
            return raceCategory;
        }

        // PUT: api/RaceCategories/5
        // Updates an existing race category.

        [Authorize(Roles = "Organiser")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRaceCategory(int id, RaceCategory raceCategory)
        {
            // Checks whether the URL ID matches the category ID.
            if (id != raceCategory.RaceCategoryId)
            {
                return BadRequest();
            }

            // Finds the category that needs to be updated.
            var existingCategory = await _context.RaceCategories.FindAsync(id);

            // Checks whether the category exists.
            if (existingCategory == null)
            {
                return NotFound();
            }

            // Checks whether the selected race event exists.
            var raceEventExists = await _context.RaceEvents
                .AnyAsync(e => e.RaceEventId == raceCategory.RaceEventId);

            if (!raceEventExists)
            {
                return BadRequest("The selected race event does not exist.");
            }

            // Updates the category details.
            existingCategory.CategoryName = raceCategory.CategoryName;
            existingCategory.CategoryDescription = raceCategory.CategoryDescription;
            existingCategory.RaceEventId = raceCategory.RaceEventId;

            // Saves the changes to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming the update.
            return NoContent();
        }

        // DELETE: api/RaceCategories/5
        // Deletes an existing race category from the database.

        [Authorize(Roles = "Organiser")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRaceCategory(int id)
        {
            // Finds the category using its ID.
            var raceCategory = await _context.RaceCategories.FindAsync(id);

            // Checks whether the category exists.
            if (raceCategory == null)
            {
                return NotFound();
            }

            // Removes the category from the database context.
            _context.RaceCategories.Remove(raceCategory);

            // Saves the deletion to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the deletion was successful.
            return NoContent();
        }


    }
}
