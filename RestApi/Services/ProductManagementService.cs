using RestApi.Data;
using RestApi.DTO;
using RestApi.Models;

namespace RestApi.Services
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly List<Product> _products;
        public ProductManagementService(IDefaultProductProvider productProvider)
        {
            _products = productProvider.Init();
        }
        public ProductDto Add(ProductDto productDto)
        {
            ArgumentNullException.ThrowIfNull(productDto, nameof(productDto));
            if (string.IsNullOrWhiteSpace(productDto.Code))
                throw new ArgumentException("Kod produktu jest wymagany.", nameof(productDto));

            if (string.IsNullOrWhiteSpace(productDto.Name))
                throw new ArgumentException("Nazwa produktu jest wymagana.", nameof(productDto));

            if (productDto.Price <= 0)
                throw new ArgumentOutOfRangeException(nameof(productDto), "Cena musi być większa od 0.");

            return MapToDto(new Product
            {
                Id = productDto.Id == Guid.Empty ? Guid.NewGuid() : productDto.Id,
                Kod = productDto.Code.Trim(),
                Nazwa = productDto.Name.Trim(),
                Cena = productDto.Price
            });
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _products.Select(MapToDto).ToList();
        }

        public IEnumerable<ProductDto> GetProductsByName(string name)
        {
            return _products
                .Where(x => x.Nazwa == name)
                .Select(MapToDto)
                .ToList();
        }

        private static ProductDto MapToDto(Product product) => new()
        {
            Id = product.Id,
            Code = product.Kod,
            Name = product.Nazwa,
            Price = product.Cena
        };
    }
}
