using IdentityServer4.Models;
using IdentityService.Domain;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;

namespace IdentityService.Infrastructure
{
    public class ClientService
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "catalog-client",
                    ClientName = "Catalog Microservice Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new IdentityServer4.Models.Secret("catalog-secret".Sha256()) },
                    AllowedScopes = { "catalog.api" }
                },
                new Client
                {
                    ClientId = "basket-client",
                    ClientName = "Basket Microservice Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new IdentityServer4.Models.Secret("basket-secret".Sha256()) },
                    AllowedScopes = { "basket.api" }
                },
                new Client
                {
                    ClientId = "order-client",
                    ClientName = "Order Microservice Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new IdentityServer4.Models.Secret("order-secret".Sha256()) },
                    AllowedScopes = { "order.api" }
                }
            };
        }
    }
}
