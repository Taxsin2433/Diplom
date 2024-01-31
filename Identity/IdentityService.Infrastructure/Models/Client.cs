using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace Identity.IdentityService.Infrastructure.Models
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "catalog-client",
                    ClientSecrets = { new Secret("catalog-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "catalog-api" }
                },
                new Client
                {
                    ClientId = "basket-client",
                    ClientSecrets = { new Secret("basket-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "basket-api" }
                },
                new Client
                {
                    ClientId = "order-client",
                    ClientSecrets = { new Secret("order-secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "order-api" }
                },
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
    }
}
