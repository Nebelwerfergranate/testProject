using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Models
{
    public class OperationResult
    {
        public Boolean Success { get; set; }
        public String Message { get; set; }
        public Boolean NeedRefresh { get; set; }
    }
}