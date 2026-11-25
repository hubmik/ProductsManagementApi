using RestApi.Models;

namespace RestApi.Data
{
    public class DefaultProductProvider : IDefaultProductProvider
    {
        public List<Product> Init()
        {
            return new List<Product>()
            {
                new() { Id = Guid.NewGuid(), Kod = "ME-01", Nazwa = "Laptop Lestaro", Cena = 3999.99m },
                new() { Id = Guid.NewGuid(), Kod = "ME-02", Nazwa = "Monitor Ferrariri", Cena = 349.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-03", Nazwa = "Wirówka do sałaty", Cena = 49.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-04", Nazwa = "USB typu CD", Cena = 39.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-05", Nazwa = "Ekspres do kawy", Cena = 600.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-06", Nazwa = "Kabelki różnej maści", Cena = 99.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-07", Nazwa = "Suszarka", Cena = 229.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-08", Nazwa = "Odmrażacz do wszystkiego", Cena = 19.99m },
                new() { Id = Guid.NewGuid(), Kod = "ME-09", Nazwa = "Ocieplacz", Cena = 60.00m },
                new() { Id = Guid.NewGuid(), Kod = "ME-10", Nazwa = "Ozimniacz", Cena = 59.99m }
            };
        }
    }
}
