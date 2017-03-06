using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models.Product
{
    public class GetPerUseroperationsSumViewModel
    {
        public String Operator { get; set; }        
        public Int64 Count { get; set; }
        public Decimal Price { get; set; }
        public Decimal Sum
        {
            get
            {
                return Count * Price;
            }
        }
    }
}