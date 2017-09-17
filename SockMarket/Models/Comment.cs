using System;
using System.ComponentModel.DataAnnotations;

namespace SockMarket.Models
{
    public class Comment
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public string Author { get; set; }
    }
}