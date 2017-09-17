using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{

    public enum Stage
    {
        InitialContact, Decision, Offer, Bill, Payment
    }

    public class Deal
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        [DisplayName("Creation time")]
        public DateTime CreationTime { get; set; }
        [Required]
        public Stage Stage { get; set; }
        [ScaffoldColumn(false)]
        public int CompanyID { get; set; }

        [DisplayName("Company Name")]
        public virtual Company Company { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}