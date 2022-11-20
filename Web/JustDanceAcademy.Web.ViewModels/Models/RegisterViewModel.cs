using System.ComponentModel.DataAnnotations;

namespace JustDanceAcademy.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]

        public string Password { get; set; } = null!;


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password doesn't match, Type again !")]

        public string ConfirmPassword { get; set; } = null!;


        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;
    }
}

