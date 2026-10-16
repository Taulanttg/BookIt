namespace BookIt.Api.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Artist { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int TotalSeats { get; set; }
        public decimal Price { get; set; }

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
    }
}
