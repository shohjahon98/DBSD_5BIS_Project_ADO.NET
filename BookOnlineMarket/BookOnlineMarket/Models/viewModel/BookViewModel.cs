using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookOnlineMarket.Models.viewModel
{
    public class BookViewModel
    {
        public IEnumerable<Book> GetBooks { get; set; }
        public PageInfo pageInfo { get; set; }
    }
}