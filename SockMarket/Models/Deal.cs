namespace SockMarket.Models
{

    public enum Stage
    {
        InitialContact, Decision, Offer, Bill, Payment
    }

    public class Deal
    {
        public int ID { get; set; }
        public Stage Stage { get; set; }
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
    }
}