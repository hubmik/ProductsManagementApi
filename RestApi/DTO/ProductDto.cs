using System.ComponentModel.DataAnnotations;

namespace RestApi.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Kod produktu jest wymagany.")]
        [StringLength(20, ErrorMessage = "Kod może mieć maksymalnie 20 znaków.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        [StringLength(255, ErrorMessage = "Nazwa może mieć maksymalnie 255 znaków.")]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0.")]
        public decimal Price { get; set; }
    }
}
