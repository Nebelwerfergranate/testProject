using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sandbox_Products.Utils
{
    public class RangeDecimalAttribute : RangeAttribute
    {
        public RangeDecimalAttribute(decimal minimum, decimal maximum): base(typeof(Decimal), minimum.ToString(), maximum.ToString()) {
        }
    }
}