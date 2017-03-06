using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sandbox_Products.Models;
using Sandbox_Products.Constants;
using System.Web.Security;
using Sandbox_Products.Models.Account;
using Sandbox_Products.BLL.Account;
using Sandbox_Products.Utils;
using Messages = Sandbox_Products.Constants.Messages;

namespace Sandbox_Products.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private String _userRedirectAction;
        private String _userRedirectController;
        
        public AccountController()
        {
            _userRedirectAction = "Index";
            _userRedirectController = "Product";
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(_userRedirectAction, _userRedirectController);
            }

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            Boolean userExists = false;
            Boolean canLogin = false;
            String message = String.Empty;
            if (ModelState.IsValid)
            {
                userExists = Membership.GetUser(model.Login) != null;
                if (userExists)
                {
                    canLogin = Membership.ValidateUser(model.Login, model.Password);
                    if (canLogin)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    }
                    else
                    {
                        message = Messages.Account.INVALID_PASSWORD_MESSAGE;
                    }
                } else
                {
                    message =  Messages.Account.GetUserNotFoundMessage(model.Login);
                }
            }
            else
            {
                message = Messages.Shared.VALIDATION_ERROR_MESSAGE;
            }

            return Json(new
            {
                result = canLogin,
                message = message,
                returnUrl = Url.Action(_userRedirectAction, _userRedirectController)
            });
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            Boolean result = false;
            String message = String.Empty;
            String redirectUrl = String.Empty;
            // todo - move try catch to manager
            if (ModelState.IsValid)
            {
                try
                {
                    AccountManager accountManager = new AccountManager();
                    message = accountManager.Register(model).Message;
                    FormsAuthentication.SetAuthCookie(model.Login, false);
                    result = true;
                    redirectUrl = Url.Action(_userRedirectAction, _userRedirectController);
                }
                catch (RegisterException ex)
                {
                    message = ex.Message;
                }
                catch (Exception ex)
                {
                    message = Messages.Account.REGISTRATION_ERROR_MESSAGE;
                    Debug.LogError(ex);
                }
            }
            else
            {
                message = Messages.Shared.VALIDATION_ERROR_MESSAGE;
            }

            return Json(new
            {
                result = result,
                message = message,
                redirectUrl = redirectUrl
            });
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}