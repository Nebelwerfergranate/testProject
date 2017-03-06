using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.BLL.Account
{
    public class RegisterException : ApplicationException
    {
        public RegisterException(String message) : base(message) { }
    }
}