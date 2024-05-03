using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.RequestModels.GuestRequestModels;
using HotelManagement.Services;


namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private IGuestService guestService;
        public CustomerController(IGuestService guestService)
        { 
            this.guestService = guestService;
        }

        //Guest

        [HttpGet("GetGuests")]
        public IActionResult GetAllGuests()
        {
            var model = guestService.GetAllGuests();
            return Ok(model);
        }

        [HttpPost("AddGuest")]
        public IActionResult InsertGuest(CreateGuestModel request)
        {
            Guest guest = new Guest
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };
            guestService.InsertGuest(guest);
            return Ok(guest.Id);
        }

        [HttpPost("ChangeGuest")]
        public IActionResult UpdateGuest(UpdateGuestModel request)
        {
            var model = guestService.GetGuestById(request.Id);

            if (model == null)
                return BadRequest("Undefined Id");

            Guest guest = new Guest
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            guestService.UpdateGuest(guest);
            return Ok(guest.Id);
        }

        [HttpGet("RemoveGuest")]
        public IActionResult DeleteGuest(int id)
        {
            var finalId = guestService.GetGuestById(id);
            if (finalId != null)
            {
                guestService.DeleteGuest(id);
                return Ok();
            }
            else
            {
                return BadRequest("Id is not defined");
            }
        }


        [HttpGet("GetGuestById")]
        public IActionResult GetGuestById(int id)
        {
            var model = guestService.GetGuestById(id);
            if (model != null)
                return Ok(model);

            return BadRequest("Id is not defined");
        }

    }
}