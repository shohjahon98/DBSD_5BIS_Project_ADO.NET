using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string City { get; set; }
        [Required]
        [StringLength(150)]
        [DisplayName("Address")]
        public string Adress { get; set; }
        [Required]
        [StringLength(13)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number. Examle: (xx)xxxxxxx")]
        public string Phone { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}