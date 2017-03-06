using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sandbox_Products.Models.Account;
using System.Web.Security;
using Sandbox_Products.Constants.Messages;

namespace Sandbox_Products.BLL.Account
{
    public class AccountManager
    {
        public RegisterResult Register(RegisterModel model)
        {
            RegisterResult result = new RegisterResult();

            MembershipUser user;
            user = Membership.GetUser(model.Login);

            if (user == null)
            {
                user = Membership.CreateUser(model.Login, model.Password);
                result.Message = Constants.Messages.Account.GetRegistrationSuccessMessage(user.UserName);
            }
            else
            {
                throw new RegisterException(Constants.Messages.Account.GetUserExistsMessage(user.UserName));
            }

            return result;
        }
    }
}