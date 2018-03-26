using System.ComponentModel.DataAnnotations;

namespace OrdersHenriqueSD.Models
{
    public class ProductOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Product Id must be informed.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "The Price must be informed.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Quantity must be informed.")]
        public int Quantity { get; set; }

        public int OrderId { get; set; }
    }
}
