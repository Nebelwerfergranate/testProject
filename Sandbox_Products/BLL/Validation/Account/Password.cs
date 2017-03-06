using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Params = Sandbox_Products.Constants.Account;

namespace Sandbox_Products.BLL.Validation.Account
{
    public class Password : ValidationProperty
    {
        //public static Boolean Required { get; set; } = GetBoolProp(Constants.PASSWORD_REQUIRED);
        //public static Int32 Minlength { get; set; } = GetInt32Prop(Constants.PASSWORD_MIN_LENGTH);
        //public static Int32 Maxlength { get; set; } = GetInt32Prop(Constants.PASSWORD_MAX_LENGTH);

        public Boolean Required { get; set; } = Params.PASSWORD_REQUIRED;
        public Int32 Minlength { get; set; } = Params.PASSWORD_MIN_LENGTH;
        public Int32 Maxlength { get; set; } = Params.PASSWORD_MAX_LENGTH;
    }
}