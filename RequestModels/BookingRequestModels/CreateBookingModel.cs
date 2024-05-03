using HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels.BookingRequestModels
{
    public class CreateBookingModel
    {
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int RoomId { get; set; }
    }
}
