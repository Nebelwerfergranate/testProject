using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Constants.Messages
{
    public class Product
    {
        public const String PRODUCT_NOT_FOUND_ERROR_MESSAGE = "Product not found.";


        public static String GetProductExistsErrorMessage(String productName)
        {
            return String.Format("Product with name '{0}' already exists'", productName);
        }

        public static String GetProductExistDeletedErrorMessage(String productName)
        {
            return String.Format("Product with name '{0}' exists but it is deleted", productName);
        }

        public static String GetNotEnoughProductsCountMessage(Int64 count)
        {
            return String.Format("Can not complete this operation because product count is {0}.", count);
        }

        public static String GetProductIsDeletedErrorMessage(String productName)
        {
            return String.Format("Product with name {0} has been deleted. Please reload page.", productName);
        }
    }
}