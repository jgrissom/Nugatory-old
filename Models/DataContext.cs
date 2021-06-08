using Microsoft.EntityFrameworkCore;

namespace WordApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<WordColor> WordColors { get; set; }
    }
}
