// Clients.cs
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService.Domain
{
    public static class Clients
    {
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "CatalogClient",
                    ClientSecrets = { new Secret("CatalogSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "CatalogAPI" }
                },
                new Client
                {
                    ClientId = "BasketClient",
                    ClientSecrets = { new Secret("BasketSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "BasketAPI" }
                },
                new Client
                {
                    ClientId = "OrderClient",
                    ClientSecrets = { new Secret("OrderSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5003/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
                    AllowedScopes = { "openid", "profile", "OrderAPI" }
                }
            };
        }
    }
}
