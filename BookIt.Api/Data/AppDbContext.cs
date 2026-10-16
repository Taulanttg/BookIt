using Microsoft.EntityFrameworkCore;

namespace BookIt.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Venue> Venues { get; set; } = null!;

        public DbSet<Models.Table> Tables { get; set; } = null!;
        public DbSet<Models.Event> Events { get; set; } = null!;

        public DbSet<Models.Reservation> Reservations { get; set; } = null!;
    }
}
