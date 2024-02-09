using CatalogService.Services;
using CatalogService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public ApiController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("items")]
        public ActionResult<IEnumerable<CatalogItemModel>> GetCatalogItems(int page = 1, int pageSize = 10)
        {
            var catalogItems = _catalogService.GetCatalogItems(page, pageSize);
            return Ok(catalogItems);
        }

        [HttpGet("item/{id}")]
        public ActionResult<CatalogItemDetailModel> GetCatalogItemById(int id)
        {
            var catalogItem = _catalogService.GetCatalogItemById(id);

            if (catalogItem == null)
            {
                return NotFound();
            }

            return Ok(catalogItem);
        }
    }
}
