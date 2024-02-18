using System;
using System.Collections.Generic;
using System.Linq;
using BasketService.Data.Interfaces;
using BasketService.Data.Models;
using BasketService.Models;
using BasketService.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Xunit;
using BasketServiceService = BasketService.Services.BasketService;

namespace BasketService.Tests;

public class BasketServiceTests
{
    [Fact]
    public void AddItemToBasket_ShouldAddNewItem_WhenItemDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var productId = 101;
        var quantity = 3;

        var basketRepositoryMock = new Mock<IBasketRepository>();
        var basketItemRepositoryMock = new Mock<IBasketItemRepository>();
        var distributedCacheMock = new Mock<IDistributedCache>();

        var basketService = new BasketServiceService(basketRepositoryMock.Object, basketItemRepositoryMock.Object, distributedCacheMock.Object);

        var basket = new Basket { UserId = userId, BasketItems = new List<BasketItem>() };
        basketRepositoryMock.Setup(r => r.GetBasketByUserId(userId)).Returns(basket);

        // Act
        basketService.AddItemToBasket(userId, productId, quantity);

        // Assert
        Assert.Single(basket.BasketItems);
        Assert.Equal(productId, basket.BasketItems.First().ProductId);
        Assert.Equal(quantity, basket.BasketItems.First().Quantity);
        basketRepositoryMock.Verify(r => r.UpdateBasket(basket), Times.Once);
        distributedCacheMock.Verify(c => c.SetString($"Basket:{userId}", It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>()), Times.Once);
    }

    [Fact]
    public void AddItemToBasket_ShouldIncrementQuantity_WhenItemExists()
    {
        // Arrange
        var userId = 1;
        var productId = 101;
        var quantity = 3;

        var basketRepositoryMock = new Mock<IBasketRepository>();
        var basketItemRepositoryMock = new Mock<IBasketItemRepository>();
        var distributedCacheMock = new Mock<IDistributedCache>();

        var basketService = new BasketServiceService(basketRepositoryMock.Object, basketItemRepositoryMock.Object, distributedCacheMock.Object);

        var basket = new Basket { UserId = userId, BasketItems = new List<BasketItem> { new BasketItem { ProductId = productId, Quantity = 2 } } };
        basketRepositoryMock.Setup(r => r.GetBasketByUserId(userId)).Returns(basket);

        // Act
        basketService.AddItemToBasket(userId, productId, quantity);

        // Assert
        Assert.Single(basket.BasketItems);
        Assert.Equal(productId, basket.BasketItems.First().ProductId);
        Assert.Equal(5, basket.BasketItems.First().Quantity); // 2 (existing) + 3 (new)
        basketRepositoryMock.Verify(r => r.UpdateBasket(basket), Times.Once);
        distributedCacheMock.Verify(c => c.SetString($"Basket:{userId}", It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>()), Times.Once);
    }

    [Fact]
    public void RemoveItemFromBasket_ShouldRemoveItem_WhenItemExists()
    {
        // Arrange
        var userId = 1;
        var productId = 101;

        var basketRepositoryMock = new Mock<IBasketRepository>();
        var basketItemRepositoryMock = new Mock<IBasketItemRepository>();
        var distributedCacheMock = new Mock<IDistributedCache>();

        var basketService = new BasketServiceService(basketRepositoryMock.Object, basketItemRepositoryMock.Object, distributedCacheMock.Object);

        var basket = new Basket { UserId = userId, BasketItems = new List<BasketItem> { new BasketItem { ProductId = productId, Quantity = 2 } } };
        basketRepositoryMock.Setup(r => r.GetBasketByUserId(userId)).Returns(basket);

        // Act
        basketService.RemoveItemFromBasket(userId, productId);

        // Assert
        Assert.Empty(basket.BasketItems);
        basketRepositoryMock.Verify(r => r.UpdateBasket(basket), Times.Once);
        distributedCacheMock.Verify(c => c.SetString($"Basket:{userId}", It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>()), Times.Once);
    }

    [Fact]
    public void RemoveItemFromBasket_ShouldDoNothing_WhenItemDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var productId = 101;

        var basketRepositoryMock = new Mock<IBasketRepository>();
        var basketItemRepositoryMock = new Mock<IBasketItemRepository>();
        var distributedCacheMock = new Mock<IDistributedCache>();

        var basketService = new BasketServiceService(basketRepositoryMock.Object, basketItemRepositoryMock.Object, distributedCacheMock.Object);

        var basket = new Basket { UserId = userId, BasketItems = new List<BasketItem> { new BasketItem { ProductId = 102, Quantity = 2 } } };
        basketRepositoryMock.Setup(r => r.GetBasketByUserId(userId)).Returns(basket);

        // Act
        basketService.RemoveItemFromBasket(userId, productId);

        // Assert
        Assert.Single(basket.BasketItems);
        basketRepositoryMock.Verify(r => r.UpdateBasket(basket), Times.Never);
        distributedCacheMock.Verify(c => c.SetString($"Basket:{userId}", It.IsAny<string>(), It.IsAny<DistributedCacheEntryOptions>()), Times.Never);
    }
}
