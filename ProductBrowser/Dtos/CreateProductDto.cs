using System.ComponentModel.DataAnnotations;

namespace ProductBrowser.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Kod produktu jest wymagany.")]
        public string Kod { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        public string Nazwa { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być większa od 0.")]
        public decimal Cena { get; set; }
    }
}
