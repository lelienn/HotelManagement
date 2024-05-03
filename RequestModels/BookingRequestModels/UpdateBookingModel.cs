using HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.RequestModels.BookingRequestModels
{
    public class UpdateBookingModel
    {
        [Required]
        public int Id { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal Price { get; set; }
    }
}
