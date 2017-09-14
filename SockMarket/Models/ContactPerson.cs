using System.Collections.Generic;

namespace SockMarket.Models
{
    public class ContactPerson
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}