using System;
using System.ComponentModel.DataAnnotations;

namespace OrdersHenriqueSD.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Name must be minimum 3 characters")]
        [MaxLength(150, ErrorMessage = "The Name must be maximum 100 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "The Name must be minimum 6 characters")]
        [MaxLength(150, ErrorMessage = "The Name must be maximum 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price must be informed")]
        public decimal Price { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
