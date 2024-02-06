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
        public ActionResult<IEnumerable<CatalogItemModel>> GetCatalogItems()
        {
            var catalogItems = _catalogService.GetAllCatalogItems();
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
