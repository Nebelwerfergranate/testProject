using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Constants
{
    public class Product
    {
        // Now I can't move it to config section because of validation attribues. They doesn't take runtime values.
        public const Boolean PRODUCT_NAME_REQUIRED = true;
        public const Int32 PRODUCT_NAME_MIN_LENGTH = 3;
        public const Int32 PRODUCT_NAME_MAX_LENGTH = 50;
        public const String PRODUCT_NAME_PATTERN = "^[A-z0-9 \"().,-]*$";

        public const Boolean PRODUCT_PRICE_REQUIRED = true;

        public const Decimal PRODUCT_PRICE_MIN = 0;
        public const Decimal PRODUCT_PRICE_MAX = 10000;
        public const String PRODUCT_PRICE_MIN_STR = "0";
        public const String PRODUCT_PRICE_MAX_STR = "10000"; // Attribute constructor doesn't allow Decimal.

        public const String RECEIPT_ID = "receipt";
        public const String EXPENSE_ID = "expense";
    }
}