using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sandbox_Products.Utils.Formatters;

namespace Sandbox_Products.Models.Product
{
    public class GetProductOperationsViewModel
    {
        public String Operator { get; set; }
        public String Operation { get; set; }
        public Int64 Count { get; set; }
        public DateTime DateRaw { get; set; }
        public String Color { get; set; }
        public String DisplayColor
        {
            get
            {
                if(Count == 0)
                {
                    return Constants.ProductOperation.ZERO_COUNT_COLOR;
                }

                return Color;
            }
        }
        public String Date {
            get
            {
                return DateTimeFormatter.GetDateTimeFormatted(DateRaw);
            }
        }

        // filter
        public String OperatorStrict { get; set; }
        public DateTime DateFrom { get; set; } = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        public DateTime DateTo { get; set; } = DateTime.MaxValue;
    }
}