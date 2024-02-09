using System.Collections.Generic;
using System.Linq;
using CatalogService.Data.Models;
using CatalogService.Services;
using CatalogService.Repository;
using Moq;
using CatalogServiceService = CatalogService.Services.CatalogService;
using Xunit;

public class CatalogServiceTests
{
    [Fact]
    public void GetAllCatalogItems_ReturnsCorrectCatalogItems()
    {
        // Arrange
        var mockRepository = new Mock<ICatalogRepository>();
        mockRepository.Setup(repo => repo.GetAllCatalogItems()).Returns(new List<CatalogItem>
        {
            new CatalogItem { Id = 1, Name = "Item1", Price = 10.99m },
            new CatalogItem { Id = 2, Name = "Item2", Price = 15.99m },
        });

        var catalogService = new CatalogServiceService(mockRepository.Object);

        // Act
        var result = catalogService.GetAllCatalogItems();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public void GetAllCatalogItems_ReturnsEmptyList_WhenNoCatalogItems()
    {
        // Arrange
        var mockRepository = new Mock<ICatalogRepository>();
        mockRepository.Setup(repo => repo.GetAllCatalogItems()).Returns(new List<CatalogItem>());

        var catalogService = new CatalogServiceService(mockRepository.Object);

        // Act
        var result = catalogService.GetAllCatalogItems();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetCatalogItemById_ReturnsCorrectCatalogItem()
    {
        // Arrange
        var mockRepository = new Mock<ICatalogRepository>();
        mockRepository.Setup(repo => repo.GetCatalogItemById(1)).Returns(new CatalogItem
        {
            Id = 1,
            Name = "Item1",
            Description = "Description for Item1",
            Price = 10.99m,
            Category = "Category1"
        });

        var catalogService = new CatalogServiceService(mockRepository.Object);

        // Act
        var result = catalogService.GetCatalogItemById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Item1", result.Name);
    }

    [Fact]
    public void GetCatalogItemById_ReturnsNull_WhenItemNotFound()
    {
        // Arrange
        var mockRepository = new Mock<ICatalogRepository>();
        mockRepository.Setup(repo => repo.GetCatalogItemById(1)).Returns((CatalogItem)null);

        var catalogService = new CatalogServiceService(mockRepository.Object);

        // Act
        var result = catalogService.GetCatalogItemById(1);

        // Assert
        Assert.Null(result);
    }
}
