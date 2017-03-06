using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Params = Sandbox_Products.Constants.Product;

namespace Sandbox_Products.BLL.Validation.Product
{
    public class ProductName : ValidationProperty
    {
        //public static Boolean Required { get; set; } = GetBoolProp(Constants.PRODUCT_NAME_REQUIRED);
        //public static Int32 Minlength { get; set; } = GetInt32Prop(Constants.PRODUCT_NAME_MIN_LENGTH);
        //public static Int32 Maxlength { get; set; } = GetInt32Prop(Constants.PRODUCT_NAME_MAX_LENGTH);
        //public static String Regex { get; set; } = GetStringProp(Constants.PRODUCT_NAME_REGEX);

        public Boolean Required { get; set; } = Params.PRODUCT_NAME_REQUIRED;
        public Int32 Minlength { get; set; } = Params.PRODUCT_NAME_MIN_LENGTH;
        public Int32 Maxlength { get; set; } = Params.PRODUCT_NAME_MAX_LENGTH;
        public String Regex { get; set; } = Params.PRODUCT_NAME_PATTERN;
    }
}