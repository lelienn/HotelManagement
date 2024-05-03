using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.RequestModels.RoomRequestModels;
using HotelManagement.Models.Constants;
using HotelManagement.Services;

namespace HotelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoomController : ControllerBase
    {
        private IRoomService roomService;
        public RoomController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        //Room

        [HttpGet("GetRooms")]
        public IActionResult GetAllRooms()
        {
            var model = roomService.GetAllRooms();
            return Ok(model);
        }

        [HttpPost("AddRoom")]
        public IActionResult InsertRoom(CreateRoomModel request)
        {
            List< RoomImage > roomImages = new List< RoomImage >();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "C:\\Users\\User\\RoomImages");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

            request.Files.ForEach(x =>
            {
                //get file extension
                FileInfo fileInfo = new FileInfo(x.FileName);
                string fileName = Guid.NewGuid() + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    x.CopyTo(stream);
                }
            roomImages.Add(new RoomImage { ImagePath= fileNameWithPath });
            });
           
            
            Room room = new Room
            {
                RoomNumber = request.RoomNumber,
                MaxPersons = request.MaxPersons,
                RoomImages= roomImages
            };
            roomService.InsertRoom(room);
            return Ok(room.Id);
        }

        [HttpPost("ChangeRoom")]
        public IActionResult UpdateRoom(UpdateRoomModel request)
        {
            var model = roomService.GetRoomById(request.Id);

            if (model == null)
                return BadRequest("Undefined Id");

            Room room = new Room
            {
                Id = request.Id,
                RoomNumber = request.RoomNumber,
                MaxPersons = request.MaxPersons,
                Status = request.Status,
            };

            roomService.UpdateRoom(room);
            return Ok(room.Id);
        }

        [HttpGet("RemoveRoom")]
        public IActionResult DeleteRoom(int id)
        {
            var finalId = roomService.GetRoomById(id);
            if (finalId != null)
            {
                roomService.DeleteRoom(id);
                return Ok();
            }
            else
            {
                return BadRequest("Id is not defined");
            }
        }


        [HttpGet("GetRoomById")]
        public IActionResult GetRoomById(int id)
        {
            var model = roomService.GetRoomById(id);
            if (model != null)
                return Ok(model);

            return BadRequest("Id is not defined");
        }

        [HttpGet("GetRoomByStatus")]
        public IActionResult GetTemplateByType(RoomStatus status)
        {
            var model = roomService.GetRoomByStatus(status);
            if (model != null)
                return Ok(model);

            return BadRequest("Type is not defined");
        }
    }
}