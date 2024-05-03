using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.RequestModels.BookingRequestModels;

namespace HotelManagement.Controllers
{
    [Authorize(Policy = "RequireRole")]
    [Route("api/[controller]")]
    [ApiController]

    public class BookingController : BaseController
    {
        private IBookingService bookingService;
        private IRoomService roomService;
        private IGuestService guestService;
        public BookingController(IBookingService bookingService,IRoomService roomService,IGuestService guestService)
        {
            this.bookingService = bookingService;
            this.roomService = roomService;
            this.guestService = guestService;
        }

        //Booking



        [HttpGet("GetBookings")]

        public IActionResult GetAllBookings()
        {
            var model = bookingService.GetAllBookings();
            return Ok(model);
        }

        [HttpPost("AddBooking")]
        public IActionResult InsertBooking(CreateBookingModel request)
        {
            try
            {
                var room = roomService.GetRoomById(request.RoomId);
                if (room == null)
                    return BadRequest("Undefined room");

                var guest = guestService.GetGuestById(UserId);
                if (guest == null)
                    return BadRequest("Undefined guest");

                Booking booking = new Booking
                {
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    Price = request.Price,
                    GuestId = UserId,
                    RoomId = request.RoomId,
                    BookTime = DateTime.Now,
                };
                bookingService.InsertBooking(booking);
                return Ok(booking.Id);
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        [HttpPost("ChangeBooking")]
        public IActionResult UpdateBooking(UpdateBookingModel request)
        {

            var model = bookingService.GetBookingById(request.Id);

            if (model == null)
                return BadRequest("Undefined Id");

            Booking booking = new Booking
            {
                Id = request.Id,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                Price = request.Price,
                GuestId = UserId,
                RoomId = model.RoomId,
                BookTime = DateTime.Now,
            };
            bookingService.UpdateBooking(booking);

            return Ok(booking.Id);
        }

        [HttpGet("RemoveBooking")]
        public IActionResult DeleteBooking(int id)
        {
            var finalId = bookingService.GetBookingById(id);
            if (finalId != null)
            {
                bookingService.DeleteBooking(id);
                return Ok();
            }
            else
            {
                return BadRequest("Id is not defined");
            }
        }

        [HttpGet("GetBookingById")]
        public IActionResult GetBookingById(int id)
        {
            var model = bookingService.GetBookingById(id);
            if (model != null)
                return Ok(model);

            return BadRequest("Id is not defined");
        }

    }
}