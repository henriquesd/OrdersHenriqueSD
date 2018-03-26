using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OrdersHenriqueSD.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public List<ProductOrder> ProductOrderList { get; set; }
    }
}
