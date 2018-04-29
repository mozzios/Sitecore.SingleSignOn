using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sitecore.SingleSignOn.Models.Account
{
    public class RegisterModels
    {
        public Guid MemberId { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [MaxLength(20)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
