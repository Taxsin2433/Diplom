using CatalogService.Data.Models;
using CatalogService.Data;

namespace CatalogService.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogDbContext _dbContext;

        public CatalogRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CatalogItem> GetAllCatalogItems()
        {
            return _dbContext.CatalogItems.ToList();
        }

        public CatalogItem GetCatalogItemById(int id)
        {
            return _dbContext.CatalogItems.FirstOrDefault(ci => ci.Id == id);
        }
    }
}
