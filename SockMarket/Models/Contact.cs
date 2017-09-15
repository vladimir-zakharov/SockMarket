using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}