using System.ComponentModel.DataAnnotations;

namespace EventTracker.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}