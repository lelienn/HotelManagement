namespace HotelManagement.ResponseModels.RoomResponseModels
{
    public class UpdateRoomResponse
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int MaxPersons { get; set; }
        public short Status { get; set; }
    }
}
