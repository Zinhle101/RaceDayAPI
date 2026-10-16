
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.API.Data;
using RaceDay.API.Models;

namespace RaceDay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RaceEventsController : ControllerBase
    {
        private readonly RaceDayContext _context;

        public RaceEventsController(RaceDayContext context)
        {
            _context = context;
        }

        // GET: api/RaceEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RaceEvent>>> GetRaceEvents()
        {
            return await _context.RaceEvents.ToListAsync();
        }

        // GET: api/RaceEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RaceEvent>> GetRaceEvent(int id)
        {
            var raceEvent = await _context.RaceEvents.FindAsync(id);

            if (raceEvent == null)
            {
                return NotFound();
            }

            return raceEvent;
        }

        // POST: api/RaceEvents
        // Creates and saves a new race event in the database
        [HttpPost]
        public async Task<ActionResult<RaceEvent>> CreateRaceEvent(RaceEvent raceEvent)
        {
            // Adds the new race event to the database context
            _context.RaceEvents.Add(raceEvent);

            // Saves the new race event to the database
            await _context.SaveChangesAsync();

            // Returns a respomse showing that the event was created successfully 
            // also provides a link to retrieve the newly created event
            return CreatedAtAction(
                nameof(GetRaceEvent),
                new { id = raceEvent.RaceEventId },
                raceEvent);
        }

    }
}
