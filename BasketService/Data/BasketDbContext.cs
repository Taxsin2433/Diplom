using BasketService.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BasketService.Data
{
    public class BasketDbContext : DbContext
    {
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>()
                .HasMany(b => b.BasketItems)
                .WithOne(i => i.Basket)
                .HasForeignKey(i => i.BasketId)
                .OnDelete(DeleteBehavior.Cascade); // Удалять связанные элементы корзины при удалении корзины

            base.OnModelCreating(modelBuilder);
        }
    }
}
