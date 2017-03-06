using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.BLL.Validation
{
    public static class Constants
    {
        public const String LOGIN_REQUIRED = "loginRequired";
        public const String LOGIN_MIN_LENGTH = "loginMinLength";
        public const String LOGIN_MAX_LENGTH = "loginMaxLength";
        public const String LOGIN_REGEX = "loginRegex";

        public const String PASSWORD_REQUIRED = "passwordRequired";
        public const String PASSWORD_MIN_LENGTH = "passwordMinLength";
        public const String PASSWORD_MAX_LENGTH = "passwordMaxLength";

        public const String OPERATION_PRODUCTS_COUNT_REQUIRED = "operationProductsCountRequired";
        public const String OPERATION_PRODUCTS_COUNT_MIN = "operationProductsCountMin";
        public const String OPERATION_PRODUCTS_COUNT_MAX = "operationProductsCountMax";

        public const String PRODUCT_NAME_REQUIRED = "productNameRequired";
        public const String PRODUCT_NAME_MIN_LENGTH = "productNameMinLength";
        public const String PRODUCT_NAME_MAX_LENGTH = "productNameMaxLength";
        public const String PRODUCT_NAME_REGEX = "productNameRegex";

        public const String PRODUCT_PRICE_REQUIRED = "productPriceRequired";
        public const String PRODUCT_PRICE_MIN = "productPriceMin";
        public const String PRODUCT_PRICE_MAX = "productPriceMax";

    }
}