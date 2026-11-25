using RestApi.DTO;
using RestApi.Services;

namespace ApiTest.Helpers
{
    public class MockProductsManagementService : IProductManagementService
    {
        public List<ProductDto> Products { get; } = new()
    {
        new() { Id = Guid.NewGuid(), Code = "A-01", Name = "Alfa", Price = 10m },
        new() { Id = Guid.NewGuid(), Code = "A-02", Name = "Beta", Price = 20m },
    };

        public string? LastSearchedName { get; private set; }
        public ProductDto? LastAdded { get; private set; }

        public IEnumerable<ProductDto> GetAllProducts() => Products;

        public IEnumerable<ProductDto> GetProductsByName(string name)
        {
            LastSearchedName = name;
            return Products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public ProductDto Add(ProductDto productDto)
        {
            LastAdded = productDto;
            return productDto;
        }
    }
}
