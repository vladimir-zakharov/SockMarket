namespace SockMarket.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public int CompanyID { get; set; }
        public int ContactPersonID { get; set; }

        public virtual Company Company { get; set; }
        public virtual ContactPerson ContactPerson { get; set; }
    }
}