using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sandbox_Products.Models.Product
{
    public class ProductOperationModel
    {
        [Required]
        public Int32 ProductId { get; set; }

        [Required]
        public String ProductOperationId { get; set; }

        [Required]
        [Range(Constants.ProductOperation.PRODUCTS_COUNT_MIN, Constants.ProductOperation.PRODUCTS_COUNT_MAX)]
        public Int16 ProductsCount { get; set; }
    }
}