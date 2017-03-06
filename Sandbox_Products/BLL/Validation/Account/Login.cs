using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Params = Sandbox_Products.Constants.Account;

namespace Sandbox_Products.BLL.Validation.Account
{
    public class Login : ValidationProperty
    {
        //public static Boolean Required { get; set; } = GetBoolProp(Constants.LOGIN_REQUIRED);
        //public static Int32 Minlength { get; set; } = GetInt32Prop(Constants.LOGIN_MIN_LENGTH);
        //public static Int32 Maxlength { get; set; } = GetInt32Prop(Constants.LOGIN_MAX_LENGTH);
        //public static String Regex { get; set; } = GetStringProp(Constants.LOGIN_REGEX);
        public Boolean Required { get; set; } = Params.LOGIN_REQUIRED;
        public Int32 Minlength { get; set; } = Params.LOGIN_MIN_LENGTH;
        public Int32 Maxlength { get; set; } = Params.LOGIN_MAX_LENGTH;
        public String Regex { get; set; } = Params.LOGIN_PATTERN;
    }
}