using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string SupName { get; set; }
        [Required]
        [StringLength(50)]
        [EmailAddress]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Invalid Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(13)]
        [Phone]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number. Examle: (xx)xxxxxxx")]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Adress { get; set; }
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}