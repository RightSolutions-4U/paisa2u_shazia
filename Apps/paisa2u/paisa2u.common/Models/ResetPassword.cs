using System.ComponentModel.DataAnnotations;

namespace paisa2u.common.Models
{
    public class ResetPassword
    {
        [Required]
        [EmailAddress]
        public string  Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string Confirmpassword { get; set; }

    }
}
