using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sandbox_Products.Models.Product
{
    public class ProductModel
    {
        [Required]
        [StringLength(Constants.Product.PRODUCT_NAME_MAX_LENGTH, MinimumLength = Constants.Product.PRODUCT_NAME_MIN_LENGTH)]
        [RegularExpression(Constants.Product.PRODUCT_NAME_PATTERN)]
        public String ProductName { get; set; }

        [Required]
        [Range(typeof(Decimal), Constants.Product.PRODUCT_PRICE_MIN_STR, Constants.Product.PRODUCT_PRICE_MAX_STR)]
        public Decimal ProductPrice { get; set; }
    }
}