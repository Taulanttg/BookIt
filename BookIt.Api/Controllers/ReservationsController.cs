using BookIt.Api.Data;
using BookIt.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BookIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ReservationsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Reservation>>> GetReservations()
    {
        var reservations = await _context.Reservations.ToListAsync();
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reservation>> GetReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        return Ok(reservation);
    }

    //CreateReservation Metoda
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Reservation>> CreateReservation(Reservation reservation)
    {
        // Swagger shpesh dërgon 0 në vend të null; e kthejmë në null
        if (reservation.TableId == 0)
            reservation.TableId = null;

        if (reservation.EventId == 0)
            reservation.EventId = null;

        // Kontrollo që lidhjet ekzistojnë
        if (!await _context.Venues.AnyAsync(v => v.Id == reservation.VenueId))
            return BadRequest($"Vendi me id {reservation.VenueId} nuk ekziston.");

        if (reservation.TableId != null &&
            !await _context.Tables.AnyAsync(t => t.Id == reservation.TableId))
            return BadRequest($"Tavolina me id {reservation.TableId} nuk ekziston.");

        if (reservation.EventId != null &&
            !await _context.Events.AnyAsync(e => e.Id == reservation.EventId))
            return BadRequest($"Eventi me id {reservation.EventId} nuk ekziston.");

        // Rregullat për tavolinën
        if (reservation.TableId != null)
        {
            var table = await _context.Tables.FindAsync(reservation.TableId);

            if (reservation.Guests > table!.Capacity)
                return BadRequest($"Tavolina ka vetëm {table.Capacity} vende, ti kërkove {reservation.Guests}.");

            bool taken = await _context.Reservations.AnyAsync(r =>
                r.TableId == reservation.TableId &&
                r.ReservationDate == reservation.ReservationDate &&
                r.Status != "Cancelled");

            if (taken)
                return BadRequest("Kjo tavolinë është e rezervuar tashmë në këtë orar.");
        }

        if (reservation.EventId != null) 
        {
            var ev = await _context.Events.FindAsync(reservation.EventId);

            int booked = await _context.Reservations
                .Where(r => r.EventId == reservation.EventId && r.Status != "Cancelled")
                .SumAsync(r => r.Guests);

            int remaining = ev!.TotalSeats - booked;

            if (reservation.Guests > remaining)
                return BadRequest($"Nuk ka vende te majaftueshme. Mbeten {remaining} vende.");
        }

        if (reservation.ReservationDate < DateTime.Now)
            return BadRequest("Nuk mund te rezervosh per te kaluaren.");

        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        return Ok(reservation);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReservation(int id, Reservation updatedReservation)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        reservation.CustomerName = updatedReservation.CustomerName;
        reservation.CustomerPhone = updatedReservation.CustomerPhone;
        reservation.ReservationDate = updatedReservation.ReservationDate;
        reservation.Guests = updatedReservation.Guests;
        reservation.Status = updatedReservation.Status;
        reservation.Notes = updatedReservation.Notes;
        reservation.VenueId = updatedReservation.VenueId;
        reservation.TableId = updatedReservation.TableId;
        reservation.EventId = updatedReservation.EventId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var reservation = await _context.Reservations.FindAsync(id);

        if (reservation == null)
            return NotFound();

        _context.Reservations.Remove(reservation);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}