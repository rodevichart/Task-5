using System.ComponentModel.DataAnnotations;

namespace VideoRent.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}