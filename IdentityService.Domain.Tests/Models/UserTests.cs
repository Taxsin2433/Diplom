using IdentityService.Domain.Models;
using Xunit;

namespace IdentityService.Domain.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_Creation_Succeeds()
        {
            // Arrange
            var user = new User
            {
                Id = "1",
                UserName = "admin",
                PasswordHash = "hashedPassword" // В реальной жизни это будет хешированный пароль
            };

            // Act & Assert
            Assert.Equal("1", user.Id);
            Assert.Equal("admin", user.UserName);
            Assert.Equal("hashedPassword", user.PasswordHash);
        }
    }
}
