using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Sandbox_Products.BLL.Validation.Account;
using Sandbox_Products.BLL.Validation.Product;
using Sandbox_Products.BLL.Validation.ProductOperation;
using Sandbox_Products.Utils;

namespace Sandbox_Products.BLL.Validation
{
    public class Rules
    {
        public Login Login { get; set; } = new Login();
        public Password Password { get; set; } = new Password();
        public ConfirmPassword ConfirmPassword { get; set; } = new ConfirmPassword();

        public ProductName ProductName { get; set; } = new ProductName();
        public ProductPrice ProductPrice { get; set; } = new ProductPrice();

        public ProductsCount ProductsCount { get; set; } = new ProductsCount();

        public static String GetSerialized()
        {
            Rules rules = new Rules();

            return JsonConvert.SerializeObject(
                rules,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCaseStaticPropertyNamesContractResolver()
                }
             );
        }
    }
}