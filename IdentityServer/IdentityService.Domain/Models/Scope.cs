// Scopes.cs
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityService.Domain
{
    public static class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("CatalogAPI", "Catalog API Access"),
                new ApiScope("BasketAPI", "Basket API Access"),
                new ApiScope("OrderAPI", "Order API Access")
            };
        }
    }
}
