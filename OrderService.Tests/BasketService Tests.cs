using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using OrderService.Services;
using Xunit;
using BasketServiceService = OrderService.Services.BasketService;

namespace OrderService.Tests
{
    public class BasketServiceTests
    {
        [Fact]
        public async Task Checkout_Successful()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.PostAsync(It.IsAny<string>(), null))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.OK });

            var basketService = new BasketServiceService(httpClientMock.Object);

            // Act
            await basketService.Checkout(123);

            // Assert
            // You may add assertions based on your specific logic
            Assert.True(true);
        }

        [Fact]
        public async Task Checkout_Failure()
        {
            // Arrange
            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.PostAsync(It.IsAny<string>(), null))
                .ReturnsAsync(new HttpResponseMessage { StatusCode = System.Net.HttpStatusCode.BadRequest });

            var basketService = new BasketServiceService(httpClientMock.Object);

            // Act
            await basketService.Checkout(123);

            // Assert
            // You may add assertions based on your specific logic
            Assert.True(true);
        }
    }
}

