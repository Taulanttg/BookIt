namespace BookIt.Api.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public int VenueId { get; set; }
        public Venue? Venue { get; set; }
    }
}
