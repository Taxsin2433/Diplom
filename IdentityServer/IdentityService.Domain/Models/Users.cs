
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityService.Domain
{
    public static class Users
    {
        public static List<TestUser> Get()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "admin",
                    Password = "Ss123456"
                }
            };
        }
    }
}
