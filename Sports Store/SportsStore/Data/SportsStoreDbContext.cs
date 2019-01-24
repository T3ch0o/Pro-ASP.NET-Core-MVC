namespace SportsStore.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using SportsStore.Models;

    public class SportsStoreDbContext : IdentityDbContext<IdentityUser>
    {
        public SportsStoreDbContext(DbContextOptions<SportsStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
