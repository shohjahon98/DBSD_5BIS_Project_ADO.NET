using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        public int ClientID { get; set; }
        public string OrderDate { get; set; }
        public virtual Book Book { get; set; }
        public virtual Client Client { get; set; }
    }
}