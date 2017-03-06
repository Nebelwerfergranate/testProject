using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sandbox_Products.Models.Account
{
    public class RegisterModel
    {
        [Required]
        //[StringLength()]
        [StringLength(Constants.Account.LOGIN_MAX_LENGTH, MinimumLength = Constants.Account.LOGIN_MIN_LENGTH)]
        [RegularExpression(Constants.Account.LOGIN_PATTERN)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(Constants.Account.PASSWORD_MAX_LENGTH, MinimumLength = Constants.Account.PASSWORD_MIN_LENGTH)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}