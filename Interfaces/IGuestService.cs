using HotelManagement.Models;
using HotelManagement.RequestModels.GuestRequestModels;
using HotelManagement.ResponseModels.GuestResponseModels;

namespace HotelManagement.Interfaces
{
    public interface IGuestService
    {
        IEnumerable<GetGuestResponse> GetAllGuests();
        GetGuestResponse GetGuestById(int id);
        void InsertGuest(Guest obj);
        void UpdateGuest(Guest obj);
        void DeleteGuest(int id);
    }
}
