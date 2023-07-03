using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFruits.Areas.Identity.Data;
using MyFruits.Models;

namespace MyFruits.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyFruitsUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyFruitsUser> MyFruitsUsers { get; set; }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}


