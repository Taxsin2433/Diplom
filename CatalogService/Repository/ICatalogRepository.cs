using CatalogService.Data.Models;

namespace CatalogService.Repository
{
    public interface ICatalogRepository
    {
        IEnumerable<CatalogItem> GetAllCatalogItems();
        CatalogItem GetCatalogItemById(int id);
    }
}
