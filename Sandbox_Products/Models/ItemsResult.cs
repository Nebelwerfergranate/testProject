using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models
{
    public class ItemsResult<T>
    {
        public Boolean Success { get; set; }
        public List<T> Items { get; set; }
        public String Message { get; set; }
        public Int32 Total { get; set; }
        public Int32 Page { get; set; }
    }
}