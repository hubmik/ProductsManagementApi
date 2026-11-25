using RestApi.DTO;

namespace RestApi.Services
{
    public interface IProductManagementService
    {
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<ProductDto> GetProductsByName(string name);
        ProductDto Add(ProductDto productDto);
    }
}
