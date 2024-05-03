using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels.RoomRequestModels
{
    public class CreateRoomModel
    {
        [Required]
        public string RoomNumber { get; set; } = null!;
        [Required]
        public int MaxPersons { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
