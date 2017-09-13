using System.Collections.Generic;

namespace SockMarket.Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }

}