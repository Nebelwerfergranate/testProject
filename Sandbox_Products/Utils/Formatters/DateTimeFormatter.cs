using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Utils.Formatters
{
    public class DateTimeFormatter
    {
        public static String GetDateTimeFormatted(DateTime dateTime)
        {
            return dateTime.ToString();
        }
    }
}