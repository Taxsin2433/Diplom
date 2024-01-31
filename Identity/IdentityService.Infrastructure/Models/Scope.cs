using IdentityServer4.Models;
using System.Collections.Generic;
namespace Identity.IdentityService.Infrastructure.Models

{
    public static class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("catalog-api", "Catalog API Access"),
                new ApiScope("basket-api", "Basket API Access"),
                new ApiScope("order-api", "Order API Access"),
            };
        }
    }
}
