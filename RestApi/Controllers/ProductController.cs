using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi.DTO;
using RestApi.Services;

namespace RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductManagementService _productManagementService;

        public ProductController(IProductManagementService productManagementService)
        {
            _productManagementService = productManagementService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll([FromQuery] string? name)
        {
            var products = string.IsNullOrWhiteSpace(name)
            ? _productManagementService.GetAllProducts()
            : _productManagementService.GetProductsByName(name);

            return Ok(products);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] ProductDto productDto)
        {
            if (productDto is null)
                return BadRequest("Dane produktu są wymagane.");

            var created = _productManagementService.Add(productDto);
            return Ok(created);
        }
    }
}
