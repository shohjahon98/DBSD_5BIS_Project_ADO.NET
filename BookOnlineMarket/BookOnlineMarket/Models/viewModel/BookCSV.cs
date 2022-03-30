using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BookOnlineMarket.Models.viewModel
{
    [Serializable]
    [XmlRoot("Book"), XmlType("Book")]
    public class BookCSV
    {
        public int Id { get; set; }
        
        public string BookName { get; set; }
        
        public string Author { get; set; }
        
        public string Publisher { get; set; }
        
        public decimal Price { get; set; }
        public string AddDate { get; set; }
        
        public string Genre { get; set; }
        public int Quentity { get; set; }
        
        public string BookImg { get; set; }
       
        public string Title { get; set; }
    }
}