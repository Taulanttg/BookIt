using BookIt.Api.Data;
using BookIt.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace BookIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VenuesController : ControllerBase
{
    private readonly AppDbContext _context;

    public VenuesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Venue>>> GetVenues()
    {
        var venues = await _context.Venues.ToListAsync();
        return Ok(venues);
    }

    [HttpPost]
    public async Task<ActionResult<Venue>> CreateVenue(Venue venue)
    {
        _context.Venues.Add(venue);
        await _context.SaveChangesAsync();
        return Ok(venue);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Venue>> GetVenue(int id)
    { 
        var venue = await _context.Venues.FindAsync(id);

        if (venue == null)
            return NotFound();

        return Ok(venue);
    
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVenue(int id) 
    {
        var venue = await _context.Venues.FindAsync(id);

        if (venue == null)
            return NotFound();

        _context.Venues.Remove(venue);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVenue(int id, Venue updateVenue)
    {

        var venue = await _context.Venues.FindAsync(id);

        if (venue == null)
            return NotFound();

        venue.Name = updateVenue.Name;
        venue.City = updateVenue.City;
        venue.Address = updateVenue.Address;
        venue.Description = updateVenue.Description;

        await _context.SaveChangesAsync();

        return NoContent();

    }
}