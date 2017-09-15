using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{
    public class Company
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }

}