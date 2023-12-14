using SimpleBookstoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SimpleBookstoreAPI
{
    public class WorldDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public WorldDbContext(DbContextOptions<WorldDbContext> options)
              : base(options)
        {
        }
    }
}
