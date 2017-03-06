using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Params = Sandbox_Products.Constants.ProductOperation;

namespace Sandbox_Products.BLL.Validation.ProductOperation
{
    public class ProductsCount : ValidationProperty
    {
        //public static Boolean Required { get; set; } = GetBoolProp(Constants.OPERATION_PRODUCTS_COUNT_REQUIRED);
        //public static Int16 Min { get; set; } = GetInt16Prop(Constants.OPERATION_PRODUCTS_COUNT_MIN);
        //public static Int16 Max { get; set; } = GetInt16Prop(Constants.OPERATION_PRODUCTS_COUNT_MAX);

        public Boolean Required { get; set; } = Params.PRODUCTS_COUNT_REQUIRED;
        public Int16 Min { get; set; } = Params.PRODUCTS_COUNT_MIN;
        public Int16 Max { get; set; } = Params.PRODUCTS_COUNT_MAX;
    }
}