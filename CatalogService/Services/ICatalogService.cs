using CatalogService.ViewModels;

namespace CatalogService.Services
{
    public interface ICatalogService
    {
        IEnumerable<CatalogItemModel> GetAllCatalogItems();
        CatalogItemDetailModel GetCatalogItemById(int id);
    }
}
