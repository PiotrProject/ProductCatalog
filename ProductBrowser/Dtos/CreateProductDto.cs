using System.ComponentModel.DataAnnotations;

namespace ProductBrowser.Dtos
{
    public class CreateProductDto
    {
        [Required]
        public string Kod { get; set; } = string.Empty;
        [Required]
        public string Nazwa { get; set; } = string.Empty;
        [Range(0.01, double.MaxValue)]
        public decimal Cena { get; set; }
    }
}
