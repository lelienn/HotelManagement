using HotelManagement.Models;

namespace HotelManagement.ResponseModels.BookingResponseModels
{
    public class GetBookingResponse
    {
        public int Id { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal Price { get; set; }

        public DateTime BookTime { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }
        public GetBookingWithGuestResponse Guest { get; set; }
        public GetBookingWithRoomResponse Room { get; set; }
    }
}
