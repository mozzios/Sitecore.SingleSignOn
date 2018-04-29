using System.ComponentModel.DataAnnotations;

namespace Sitecore.SingleSignOn.Models.Account
{
    public class LoginModels
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
