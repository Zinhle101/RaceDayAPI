
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

            // Returns a response showing that the event was created successfully 
            // also provides a link to retrieve the newly created event
            return CreatedAtAction(
                nameof(GetRaceEvent),
                new { id = raceEvent.RaceEventId },
                raceEvent);
        }
        // PUT: api/RaceEvents/5
        // Updates the details of an existing race event.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRaceEvent(int id, RaceEvent raceEvent)
        {
            // Checks whether the event ID in the URL matches the event being updated.
            if (id != raceEvent.RaceEventId)
            {
                return BadRequest();
            }

            // Checks whether the event exists in the database.
            var existingEvent = await _context.RaceEvents.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            // Updates the event details.
            existingEvent.EventName = raceEvent.EventName;
            existingEvent.EventDescription = raceEvent.EventDescription;
            existingEvent.Location = raceEvent.Location;
            existingEvent.EventDate = raceEvent.EventDate;

            // Saves the changes to the database.
            await _context.SaveChangesAsync();

            // Returns a response confirming that the update was successful.
            return NoContent();
        }
    }
}
