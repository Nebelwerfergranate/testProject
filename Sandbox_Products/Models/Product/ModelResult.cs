using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models.Product
{
    public class ModelResult<T>
    {
        public T Model { get; set; }
        public Boolean Success { get; set; }
        public String Message { get; set; }
    }
}