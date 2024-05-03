using HotelManagement.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels.GuestRequestModels
{
    public class CreateGuestModel
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        [RegularExpression(ValidationRegexValues.PhoneRegex, ErrorMessage = ValidationRegexValues.PhoneErrorMessage)]
        public string Phone { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [RegularExpression(ValidationRegexValues.PasswordRegex, ErrorMessage = ValidationRegexValues.PasswordErrorMessage)]
        public string Password { get; set; } = null!;
    }
}
