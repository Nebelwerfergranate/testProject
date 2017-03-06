using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Sandbox_Products.Constants.Messages;
using Sandbox_Products.Utils;

namespace Sandbox_Products.Models.Product
{
    public class UpdateProductModel
    {
        [Required]
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        [StringLength(Constants.Product.PRODUCT_NAME_MAX_LENGTH, MinimumLength = Constants.Product.PRODUCT_NAME_MIN_LENGTH)]
        [RegularExpression(Constants.Product.PRODUCT_NAME_PATTERN, ErrorMessage = Shared.REGEX_DEFAULT_VALIDATIONS_ERROR_MESSAGE)]
        public String Name { get; set; }

        [Required]
        [Range(typeof(Decimal), Constants.Product.PRODUCT_PRICE_MIN_STR, Constants.Product.PRODUCT_PRICE_MAX_STR)]
        public Decimal Price { get; set; }
    }
}