using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string BookName { get; set; }
        [Required]
        [StringLength(100)]
        public string Author { get; set; }
        [Required]
        [StringLength(60)]
        public string Publisher { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string AddDate { get; set; } 
        [Required]
        [StringLength(50)]
        public string Genre { get; set; }
        [DisplayName("Quantity")]
        public int Quentity { get; set; }
        [Column(TypeName = "image")]
        [Required]
        public byte[] BookImg { get; set; }
        [StringLength(500)]
        public string Title { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}