using System;
using System.ComponentModel.DataAnnotations;

namespace OrdersHenriqueSD.Models
{
    public class UserPortal
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "FirstName must be minimum 3 characters")]
        [MaxLength(150, ErrorMessage = "FirstName must be maximum 150 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "FirstName must be minimum 3 characters")]
        [MaxLength(25, ErrorMessage = "FirstName must be maximum 25 characters")]
        public string UserName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be minimum 6 characters")]
        [MaxLength(25, ErrorMessage = "Password must be maximum 25 characters")]
        public string Password { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "E-mail must be a maximum 100 characters")]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
