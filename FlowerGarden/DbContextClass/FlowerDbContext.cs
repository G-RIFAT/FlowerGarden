using FlowerGarden.Models.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace FlowerGarden.DbContextClass
{
    public class FlowerDbContext : DbContext
    {
        public FlowerDbContext(DbContextOptions<FlowerDbContext> options) : base(options) { }

        public DbSet<FlowerPost> FlowerPosts { get; set; }
    }
}
