namespace HotelManagement.Models.Constants
{
    public class ValidationRegexValues
    {
        public const string PasswordRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$";
        public const string PasswordErrorMessage = "Password must be between 8 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.";

        public const string PhoneRegex = "^([\\+]?374[-]?|[0])?[1-9][0-9]{7}$";
        public const string PhoneErrorMessage = "There are some problems with phone number.";
    }
}