namespace BookIt.Api.Models;

public class Reservation
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public DateTime ReservationDate { get; set; }
    public int Guests { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int VenueId { get; set; }
    public Venue? Venue { get; set; }

    public int? TableId { get; set; }
    public Table? Table { get; set; }

    public int? EventId { get; set; }
    public Event? Event { get; set; }
}