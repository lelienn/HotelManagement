using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels
{
    public class LoginRequestModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
