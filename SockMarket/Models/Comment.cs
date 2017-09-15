using System;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Time { get; set; }
        public int UserID { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}