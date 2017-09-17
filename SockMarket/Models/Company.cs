using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{
    public class Company
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [DisplayName("Company Contacts")]
        public virtual ICollection<Contact> Contacts { get; set; }
    }

}