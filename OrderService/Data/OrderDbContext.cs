using Microsoft.EntityFrameworkCore;
using OrderService.Data.Models;

namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<OrderDbModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDbModel>()
                            .HasMany(b => b.Items)
                            .WithOne(i => i.Order)
                            .HasForeignKey(i => i.OrderId)
                            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
