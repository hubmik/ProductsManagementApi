namespace RestApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena {  get; set; }
    }
}
