using CatalogService.ViewModels;

namespace CatalogService.Services
{
    public interface ICatalogService
    {
        IEnumerable<CatalogItemModel> GetCatalogItems(int page, int pageSize);
        CatalogItemDetailModel GetCatalogItemById(int id);
    }
}
