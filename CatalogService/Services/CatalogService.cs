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
            // Метод для получения всех элементов каталога
            var catalogItems = _catalogRepository.GetAllCatalogItems();

            // Преобразование сущностей в модели для возвращения
            var catalogItemModels = new List<CatalogItemModel>();
            foreach (var catalogItem in catalogItems)
            {
                catalogItemModels.Add(new CatalogItemModel
                {
                    Id = catalogItem.Id,
                    Name = catalogItem.Name,
                    Price = catalogItem.Price
                    // Добавьте другие поля, если необходимо
                });
            }

            return catalogItemModels;
        }

        public CatalogItemDetailModel GetCatalogItemById(int id)
        {
            // Метод для получения элемента каталога по идентификатору
            var catalogItem = _catalogRepository.GetCatalogItemById(id);

            // Преобразование сущности в модель для возвращения
            var catalogItemDetailModel = new CatalogItemDetailModel
            {
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                Description = catalogItem.Description,
                Price = catalogItem.Price,
                Category = catalogItem.Category
                // Добавьте другие поля, если необходимо
            };

            return catalogItemDetailModel;
        }
    }
}
