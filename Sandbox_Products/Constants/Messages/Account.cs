using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sandbox_Products.Constants.Messages
{
    public static class Account
    {
        public const String REGISTRATION_ERROR_MESSAGE = "Error occurred during registration. Try again later.";
        public const String INVALID_PASSWORD_MESSAGE = "Invalid password";

        public static String GetRegistrationSuccessMessage(string userName)
        {
            return String.Format("Registration for user {0} completed successfully.", userName);
        }

        public static String GetUserExistsMessage(string username)
        {
            return String.Format("User {0} already exists.", username);
        }

        public static String GetUserNotFoundMessage(string username)
        {
            return String.Format("User {0} not found", username);
        }
    }
}