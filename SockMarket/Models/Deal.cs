using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{

    public enum Stage
    {
        InitialContact, Decision, Offer, Bill, Payment
    }

    public class Deal
    {
        public int ID { get; set; }
        [Required]
        public DateTime Time { get; set; }
        [Required]
        public Stage Stage { get; set; }
        public int CompanyID { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}