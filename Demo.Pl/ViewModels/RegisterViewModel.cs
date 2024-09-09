using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.ViewModels
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "confirm Password does not match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
