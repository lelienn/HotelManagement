using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels.RoomRequestModels
{
    public class UpdateRoomModel
    {
        [Required]
        public int Id { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int MaxPersons { get; set; }
        public short Status { get; set; }
    }
}
