using Microsoft.EntityFrameworkCore;

namespace Practice1.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

              Database.EnsureCreated();     // creating new db when first call

        }

        public DbSet<User> Users { get; set; }

    }
}
