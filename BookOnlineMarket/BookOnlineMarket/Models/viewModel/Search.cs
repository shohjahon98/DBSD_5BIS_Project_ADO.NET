using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.viewModel
{
    public class Search
    {
        public string Author { get; set; }
        public string BookName { get; set; }
        public string SupName { get; set; }
        public string OrderDate { get; set; }
        public string ClientName { get; set; }
    }
}