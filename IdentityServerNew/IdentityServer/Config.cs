using IdentityServer4.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("alevelwebsite.com")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("mvc")
                    },
                },  
                new ApiResource("catalog")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("catalog.getitems"),
                        new Scope("catalog.getitem"),
                    },
                },
                new ApiResource("basket")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("basket.getbasket"),
                        new Scope("basket.additem"),
                        new Scope("basket.removeitem"),
                        new Scope("basket.checkout"),
                    },
                },
                new ApiResource("order")
                {
                    Scopes = new List<Scope>
                    {
                        new Scope("order.getorder"),
                        new Scope("order.placeorder"),
                        
                    },
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "mvc_pkce",
                    ClientName = "MVC PKCE Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RedirectUris = { $"{configuration["MvcUrl"]}/signin-oidc"},
                    AllowedScopes = {"openid", "profile", "mvc"},
                    RequirePkce = true,
                    RequireConsent = false
                },
                new Client
                {
                    ClientId = "catalog",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                     AllowedScopes =
                    {
                        "catalog.getitems",
                        "catalog.getitem",
                    }
                },
                new Client
                {
                    ClientId = "catalogswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc", "catalog.catalogitem"
                    }
                },
                
                new Client
                {
                    ClientId = "basket",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    
                    AllowedScopes =
                    {
                        "basket.getbasket",
                        "basket.additem",
                        "basket.removeitem",
                        "basket.checkout",
                    }
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApi"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApi"]}/swagger/" },

                    AllowedScopes =
                    {
                        "mvc"
                    }
                },
                new Client
                {
                    ClientId = "order",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                      "order.getorder",
                      "order.placeorder",
                      "basket.checkout"
                    }
                },
            };
        }
    }
}