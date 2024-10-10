using System.ComponentModel.DataAnnotations;

namespace Store.Service.BasketService
{
    public class BasketItemDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue,ErrorMessage ="Price must greater than 0")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "quantity must between 1 and 10 peices")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string BrandName { get; set; }
        [Required]
        public string TypeName { get; set; }
    }
}