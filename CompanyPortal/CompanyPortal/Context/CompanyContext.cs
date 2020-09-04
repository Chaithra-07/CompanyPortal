using CompanyPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
    }
}
