using System.ComponentModel.DataAnnotations;

namespace Authentication2F.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
