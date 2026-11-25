using ApiTest.Helpers;
using RestApi.DTO;
using RestApi.Services;

namespace ApiTest
{
    public class ProductManagementServiceTests
    {
        private static ProductManagementService CreateService() => new(new MockProductProvider());
        private static ProductDto NewDto(string code = "N-01", string name = "Nowy", decimal price = 9.99m) =>
        new() { Code = code, Name = name, Price = price };

        [Fact]
        public void GetAllProducts_ReturnsSeededProducts()
        {
            var result = CreateService().GetAllProducts().ToList();

            Assert.Equal(3, result.Count);
            Assert.Contains(result, p => p.Code == "T-01" && p.Name == "Laptop Test" && p.Price == 1000m);
        }

        [Fact]
        public void Add_ReturnsDtoWithGeneratedIdAndTrimmedValues()
        {
            var result = CreateService().Add(NewDto("  N-01  ", "  Nowy produkt  ", 12.50m));

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal("N-01", result.Code);
            Assert.Equal("Nowy produkt", result.Name);
            Assert.Equal(12.50m, result.Price);
        }

        [Fact]
        public void Add_PersistsProduct_SoItAppearsInGetAll()
        {
            var service = CreateService();

            var added = service.Add(NewDto());
            var all = service.GetAllProducts().ToList();

            Assert.Equal(4, all.Count);
            Assert.Contains(all, p => p.Id == added.Id && p.Code == "N-01");
        }

        [Fact]
        public void Add_IgnoresIdSentByClient()
        {
            var clientId = Guid.NewGuid();
            var dto = NewDto();
            dto.Id = clientId;

            var result = CreateService().Add(dto);

            Assert.NotEqual(clientId, result.Id);
        }

        [Fact]
        public void Add_TwoProducts_GetDifferentIds()
        {
            var service = CreateService();

            var first = service.Add(NewDto("A-1"));
            var second = service.Add(NewDto("A-2"));

            Assert.NotEqual(first.Id, second.Id);
        }

        [Theory]
        [InlineData("laptop", 1)]
        [InlineData("LAPTOP", 1)]
        [InlineData("test", 2)]
        [InlineData("suszar", 1)]
        [InlineData("nie-istnieje", 0)]
        public void GetProductsByName_IsCaseInsensitiveAndMatchesFragment(string name, int expectedCount)
        {
            var result = CreateService().GetProductsByName(name).ToList();

            Assert.Equal(expectedCount, result.Count);
        }
    }
}
