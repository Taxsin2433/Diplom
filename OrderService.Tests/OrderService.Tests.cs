using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Data.Interfaces;
using OrderService.Data.Models;
using OrderService.Models;
using OrderService.Services;
using OrderService.Services.Interfaces;
using OrderService.ViewModels;
using Xunit;
using OrderServiceService = OrderService.Services.OrderService;

namespace OrderService.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void PlaceOrder_Successful()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            var basketServiceMock = new Mock<IBasketService>();
            var loggerMock = new Mock<ILogger<OrderServiceService>>();

            var orderService = new OrderServiceService(
                orderRepositoryMock.Object,
                orderItemRepositoryMock.Object,
                basketServiceMock.Object,
                loggerMock.Object);

            var orderRequest = new OrderRequestModel
            {
                Basket = new BasketRequestModel
                {
                    BasketItems = new List<BasketItemRequest>
                    {
                        new BasketItemRequest { ProductId = 1, Quantity = 2 }
                    }
                }
            };

            // Act
            orderService.PlaceOrder(123, orderRequest);

            // Assert
            orderRepositoryMock.Verify(repo => repo.PlaceOrder(It.IsAny<OrderDbModel>()), Times.Once);
            basketServiceMock.Verify(basketService => basketService.Checkout(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void PlaceOrder_EmptyBasket()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            var basketServiceMock = new Mock<IBasketService>();
            var loggerMock = new Mock<ILogger<OrderServiceService>>();

            var orderService = new OrderServiceService(
                orderRepositoryMock.Object,
                orderItemRepositoryMock.Object,
                basketServiceMock.Object,
                loggerMock.Object);

            var orderRequest = new OrderRequestModel
            {
                // Empty basket
            };

            // Act
            orderService.PlaceOrder(123, orderRequest);

            // Assert
            orderRepositoryMock.Verify(repo => repo.PlaceOrder(It.IsAny<OrderDbModel>()), Times.Never);
            basketServiceMock.Verify(basketService => basketService.Checkout(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetOrderDetails_Successful()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            var basketServiceMock = new Mock<IBasketService>();
            var loggerMock = new Mock<ILogger<OrderServiceService>>();

            var orderService = new OrderServiceService(
                orderRepositoryMock.Object,
                orderItemRepositoryMock.Object,
                basketServiceMock.Object,
                loggerMock.Object);

            var orderId = 1;
            orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId))
                .Returns(new OrderDbModel
                {
                    Id = orderId,
                    UserId = 123,
                    Status = "Placed",
                    DateCreated = DateTime.Now,
                    Items = new List<OrderItemModel>
                    {
                        new OrderItemModel { CatalogItemId = 1, Quantity = 2 }
                    }
                });

            // Act
            var orderDetails = orderService.GetOrderDetails(orderId);

            // Assert
            Assert.NotNull(orderDetails);
            Assert.Equal(orderId, orderDetails.OrderId);
            Assert.Equal(123, orderDetails.UserId);
            Assert.Equal("Placed", orderDetails.Status);
            Assert.Single(orderDetails.Items);
        }

        [Fact]
        public void GetOrderDetails_OrderNotFound()
        {
            // Arrange
            var orderRepositoryMock = new Mock<IOrderRepository>();
            var orderItemRepositoryMock = new Mock<IOrderItemRepository>();
            var basketServiceMock = new Mock<IBasketService>();
            var loggerMock = new Mock<ILogger<OrderServiceService>>();

            var orderService = new OrderServiceService(
                orderRepositoryMock.Object,
                orderItemRepositoryMock.Object,
                basketServiceMock.Object,
                loggerMock.Object);

            var orderId = 1;
            orderRepositoryMock.Setup(repo => repo.GetOrderById(orderId))
                .Returns((OrderDbModel)null);

            // Act
            var orderDetails = orderService.GetOrderDetails(orderId);

            // Assert
            Assert.Null(orderDetails);
        }
    }
}
