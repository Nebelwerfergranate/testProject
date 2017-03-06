using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Constants
{
    public class Account
    {
        public const Boolean LOGIN_REQUIRED = true;
        public const Int32 LOGIN_MIN_LENGTH = 3;
        public const Int32 LOGIN_MAX_LENGTH = 255;
        public const String LOGIN_PATTERN = "^[A-z0-9]*$";

        public const Boolean PASSWORD_REQUIRED = true;
        public const Int32 PASSWORD_MIN_LENGTH = 5;
        public const Int32 PASSWORD_MAX_LENGTH = 255;
        
    }
}