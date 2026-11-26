using RestApi.Data;
using RestApi.Models;

namespace ApiTest.Helpers
{
    public class MockProductProvider : IDefaultProductProvider
    {
        public List<Product> Init() => new()
        {
            new() { Id = Guid.NewGuid(), Kod = "T-01", Nazwa = "Laptop Test", Cena = 1000m },
            new() { Id = Guid.NewGuid(), Kod = "T-02", Nazwa = "Monitor Test", Cena = 500m },
            new() { Id = Guid.NewGuid(), Kod = "T-03", Nazwa = "Suszarka", Cena = 100m },
        };
    }
}
