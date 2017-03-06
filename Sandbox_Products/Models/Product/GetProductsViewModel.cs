using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models.Product
{
    public class GetProductsViewModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Decimal Price { get; set; }        
    }
}