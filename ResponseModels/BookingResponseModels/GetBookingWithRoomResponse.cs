namespace HotelManagement.ResponseModels.BookingResponseModels
{
    public class GetBookingWithRoomResponse
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public short Status { get; set; }
    }
}
