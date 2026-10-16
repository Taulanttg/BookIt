using BookIt.Api.Data;
using BookIt.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController : ControllerBase 
{
    private readonly AppDbContext _context;
    public TablesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Table>>> GetTables()
    {
        var tables = await _context.Tables.ToListAsync();
        return Ok(tables);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Table>> GetTable(int id)
    {
        var table = await _context.Tables.FindAsync(id);
        if (table == null)
            return NotFound();
        
        return Ok(table);
    }

    [HttpPost]
    public async Task<ActionResult<Table>> CreateTable(Table table)
    {
        _context.Tables.Add(table);
        await _context.SaveChangesAsync();
        return Ok(table);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTable(int id, Table updatedTable) 
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
            return NotFound();

        table.Name = updatedTable.Name;
        table.Capacity = updatedTable.Capacity;
        table.VenueId = updatedTable.VenueId;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTable(int id)
    {
        var table = await _context.Tables.FindAsync(id);

        if (table == null)
            return NotFound();

        _context.Tables.Remove(table);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}