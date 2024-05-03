using HotelManagement.Models;

namespace HotelManagement.ResponseModels.BookingResponseModels
{
    public class UpdateBookingResponse
    {
        public int Id { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public decimal Price { get; set; }

        public DateTime BookTime { get; set; }

        public int GuestId { get; set; }

        public int RoomId { get; set; }
    }
}
