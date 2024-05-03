using HotelManagement.Models;
using HotelManagement.RequestModels;
using HotelManagement.ResponseModels;
using HotelManagement.ResponseModels.GuestResponseModels;

namespace HotelManagement.Interfaces
{
    public interface IAuthenticationnService
    {
        string GetToken(Guest customer);
        public GetGuestResponse GetUserByEmail(string email);
        public LoginResponseModel Login(LoginRequestModel request);
    }
}
