using System;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.IdentityService.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            var configurationDbContext = services.GetRequiredService<ConfigurationDbContext>();

            context.Database.Migrate();
            configurationDbContext.Database.Migrate();

            // Seed IdentityServer Clients and Scopes here
            if (!configurationDbContext.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    configurationDbContext.Clients.Add(client.ToEntity());
                }

                configurationDbContext.SaveChanges();
            }

            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    configurationDbContext.IdentityResources.Add(resource.ToEntity());
                }

                configurationDbContext.SaveChanges();
            }
        }
    }
}
