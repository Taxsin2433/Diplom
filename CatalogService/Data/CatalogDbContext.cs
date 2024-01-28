using CatalogService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Data
{

        public class CatalogDbContext : DbContext
        {
            public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
            {
            }

            public DbSet<CatalogItem> CatalogItems { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<CatalogItem>().HasKey(ci => ci.Id);


                base.OnModelCreating(modelBuilder);
            }
        }
    }


