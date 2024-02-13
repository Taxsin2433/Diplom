using CatalogService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class CatalogItemSeeder
{
    private readonly Random _random = new Random();

    public IEnumerable<CatalogItem> GenerateCatalogItems(int count)
    {
        var categories = new[] { "Electronics", "Clothing", "Books", "Home & Kitchen", "Toys", "Sports", "Beauty" };
        var names = new[] { "Widget", "Gadget", "Thingamajig", "Doohickey", "Whatchamacallit", "Contraption" };
        var descriptions = new[] { "Lorem ipsum dolor sit amet", "Consectetur adipiscing elit", "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua" };

        var catalogItems = new List<CatalogItem>();
        for (int i = 0; i < count; i++)
        {
            var catalogItem = new CatalogItem
            {
                Name = GetRandomElement(names) + " " + GetRandomSuffix(),
                Description = GetRandomElement(descriptions),
                Price = Math.Round((decimal)_random.NextDouble() * 1000, 2), // Price between 0 and 1000
                Category = GetRandomElement(categories)
            };
            catalogItems.Add(catalogItem);
        }

        return catalogItems;
    }


    private T GetRandomElement<T>(T[] array)
    {
        return array[_random.Next(array.Length)];
    }

    private string GetRandomSuffix()
    {
        return _random.Next(1000, 10000).ToString();
    }
}
