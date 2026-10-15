namespace BookIt.Api.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Monun";
        public string City { get; set; } = "Suhareke";
        public string Address { get; set; } = "Rruga Skenderbeu";
        public string? Description { get; set; } = "Atmosferë e ngrohtë familjare, specializohet me kafe, pije të ftohta dhe ushqim.";
    }
}
