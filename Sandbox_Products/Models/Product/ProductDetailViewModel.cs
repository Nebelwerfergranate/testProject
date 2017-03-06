using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models.Product
{
    public class ProductDetailViewModel
    {
        public String Name { get; set; }
        public Decimal Price { get; set; }
        public Int64 Count { get; set; }
        public Decimal TotalCost
        {
            get
            {
                return Price * Count;
            }
        }
    }
}