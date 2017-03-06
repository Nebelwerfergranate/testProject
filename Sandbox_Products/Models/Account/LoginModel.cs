using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sandbox_Products.Models.Account
{
    public class LoginModel
    {
        [Required]
        [StringLength(Constants.Account.LOGIN_MAX_LENGTH, MinimumLength = Constants.Account.LOGIN_MIN_LENGTH)]
        [RegularExpression(Constants.Account.LOGIN_PATTERN)]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Constants.Account.PASSWORD_MAX_LENGTH, MinimumLength = Constants.Account.PASSWORD_MIN_LENGTH)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}