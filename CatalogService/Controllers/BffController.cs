using CatalogService.Services;
using CatalogService.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BffController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public BffController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet("catalogItems")]
        public ActionResult<IEnumerable<CatalogItemModel>> GetCatalogItems()
        {
            var catalogItems = _catalogService.GetAllCatalogItems();
            return Ok(catalogItems);
        }

        [HttpGet("catalogItem/{id}")]
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
