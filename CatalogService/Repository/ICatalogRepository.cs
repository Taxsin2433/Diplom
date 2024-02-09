using CatalogService.Data.Models;

namespace CatalogService.Repository
{
    public interface ICatalogRepository
    {
        IEnumerable<CatalogItem> GetCatalogItems(int page, int pageSize);
        CatalogItem GetCatalogItemById(int id);
    }
}
