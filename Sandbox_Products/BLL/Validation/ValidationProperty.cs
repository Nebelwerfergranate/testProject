using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Sandbox_Products.BLL.Validation
{
    public abstract class ValidationProperty
    {
        protected static Boolean GetBoolProp(String code)
        {
            return Boolean.Parse(ConfigurationManager.AppSettings.Get(code));
        }

        protected static Int32 GetInt32Prop(String code)
        {
            return Int32.Parse(ConfigurationManager.AppSettings.Get(code));
        }
        
        protected static Int16 GetInt16Prop(string code)
        {
            return Int16.Parse(ConfigurationManager.AppSettings.Get(code));
        }

        protected static Decimal GetDecimapProp(string code)
        {
            return Decimal.Parse(ConfigurationManager.AppSettings.Get(code));
        }

        protected static String GetStringProp(String code)
        {
            return ConfigurationManager.AppSettings.Get(code);
        }
    }
}