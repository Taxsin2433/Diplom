using CatalogService.Repository;
using CatalogService.ViewModels;

namespace CatalogService.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public IEnumerable<CatalogItemModel> GetAllCatalogItems()
        {

            var catalogItems = _catalogRepository.GetAllCatalogItems();

            var catalogItemModels = new List<CatalogItemModel>();
            foreach (var catalogItem in catalogItems)
            {
                catalogItemModels.Add(new CatalogItemModel
                {
                    Id = catalogItem.Id,
                    Name = catalogItem.Name,
                    Price = catalogItem.Price
          
                });
            }

            return catalogItemModels;
        }

        public CatalogItemDetailModel GetCatalogItemById(int id)
        {

            var catalogItem = _catalogRepository.GetCatalogItemById(id);

            var catalogItemDetailModel = new CatalogItemDetailModel
            {
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                Description = catalogItem.Description,
                Price = catalogItem.Price,
                Category = catalogItem.Category
            };

            return catalogItemDetailModel;
        }
    }
}
