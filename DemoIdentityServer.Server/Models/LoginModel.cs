using System.ComponentModel.DataAnnotations;

namespace DemoIdentityServer.Server.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
