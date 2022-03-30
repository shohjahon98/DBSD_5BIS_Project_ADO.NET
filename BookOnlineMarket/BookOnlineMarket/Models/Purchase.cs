using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int SupplireID { get; set; }
        public int BookID { get; set; }
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string PurchaseDate { get; set; }
        public virtual Book Book { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}