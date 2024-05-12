using HotelManagement.Models;
using HotelManagement.ResponseModels.BookingResponseModels;

namespace HotelManagement.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<GetBookingResponse> GetAllBookings();
        GetBookingResponse GetBookingById(int id);
        void InsertBooking(Booking obj);
        void UpdateBooking(Booking obj);
        void DeleteBooking(int id);
    }
}
