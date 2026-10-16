using BookIt.Api.Data;
using BookIt.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetEvents()
    {
        var events = await _context.Events.ToListAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
            return NotFound();

        return Ok(ev);
    }

    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent(Event ev)
    {
        _context.Events.Add(ev);
        await _context.SaveChangesAsync();
        return Ok(ev);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
            return NotFound();

        ev.Name = updatedEvent.Name;
        ev.Artist = updatedEvent.Artist;
        ev.Description = updatedEvent.Description;
        ev.StartDate = updatedEvent.StartDate;
        ev.TotalSeats = updatedEvent.TotalSeats;
        ev.Price = updatedEvent.Price;
        ev.VenueId = updatedEvent.VenueId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var ev = await _context.Events.FindAsync(id);

        if (ev == null)
            return NotFound();

        _context.Events.Remove(ev);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}