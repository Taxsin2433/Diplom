using Identity.IdentityService.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityService.Domain.Interfaces;

namespace Identity.IdentityService.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly List<Client> _clients;

        public ClientRepository()
        {
            // Инициализация клиентов в памяти
            _clients = new List<Client>
            {
                new Client
                {
                    ClientId = "catalog-client",
                    ClientName = "Catalog Microservice",
                    AllowedScopes = new List<string> { "catalog-api" },
                    RedirectUris = new List<string> { "https://localhost:5001/signin-oidc" },
                    ClientSecrets = new List<Secret> { new Secret("catalog-secret".Sha256()) },
                    RequirePkce = true,
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientId = "basket-client",
                    ClientName = "Basket Microservice",
                    AllowedScopes = new List<string> { "basket-api" },
                    RedirectUris = new List<string> { "https://localhost:5002/signin-oidc" },
                    ClientSecrets = new List<Secret> { new Secret("basket-secret".Sha256()) },
                    RequirePkce = true,
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType = AccessTokenType.Jwt
                },
                new Client
                {
                    ClientId = "order-client",
                    ClientName = "Order Microservice",
                    AllowedScopes = new List<string> { "order-api" },
                    RedirectUris = new List<string> { "https://localhost:5003/signin-oidc" },
                    ClientSecrets = new List<Secret> { new Secret("order-secret".Sha256()) },
                    RequirePkce = true,
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType = AccessTokenType.Jwt
                }
            };
        }

        public Task<IEnumerable<Client>> GetClientsAsync()
        {
            return Task.FromResult(_clients.AsEnumerable());
        }
    }
}
