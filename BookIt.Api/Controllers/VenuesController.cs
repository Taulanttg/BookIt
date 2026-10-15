using BookIt.Api.Data;
using BookIt.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
}