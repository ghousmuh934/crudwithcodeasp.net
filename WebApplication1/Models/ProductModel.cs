using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Count")]
        public int Count { get; set; }
    }
}