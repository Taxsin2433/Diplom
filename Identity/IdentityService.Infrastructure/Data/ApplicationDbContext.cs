using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Reflection.Emit;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Identity.IdentityService.Infrastructure.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

        // DbSet'ы для ваших моделей (Client, Scope и другие)
        public DbSet<Client> Clients { get; set; }
        public DbSet<Scope> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Дополнительные конфигурации сущностей и связей

            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Scope>().ToTable("Scopes");
        }
    }
}
