using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Params = Sandbox_Products.Constants.Product;

namespace Sandbox_Products.BLL.Validation.Product
{
    public class ProductPrice : ValidationProperty
    {
        //public static Boolean Required { get; set; } = GetBoolProp(Constants.PRODUCT_PRICE_REQUIRED);
        //public static Decimal Min { get; set; } = GetDecimapProp(Constants.PRODUCT_PRICE_MIN);
        //public static Decimal Max { get; set; } = GetDecimapProp(Constants.PRODUCT_PRICE_MAX);

        public Boolean Required { get; set; } = Params.PRODUCT_PRICE_REQUIRED;
        public Decimal Min { get; set; } = Params.PRODUCT_PRICE_MIN;
        public Decimal Max { get; set; } = Params.PRODUCT_PRICE_MAX;
    }
}