using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.viewModel
{
    public class ProcessViewModel
    {
        public IEnumerable<Procces> Procces { get; set; }
        public PageInfo pageInfo { get; set; }
        public Search Search { get; set; }
    }
}