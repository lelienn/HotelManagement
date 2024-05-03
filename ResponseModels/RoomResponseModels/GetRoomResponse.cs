namespace HotelManagement.ResponseModels.RoomResponseModels
{
    public class GetRoomResponse
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int MaxPersons { get; set; }
        public short Status { get; set; }
        public List<string> RoomImages { get; set; }
    }
}
