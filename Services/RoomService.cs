using HotelManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Interfaces;
using HotelManagement.ResponseModels.RoomResponseModels;
using HotelManagement.Models.Constants;
using static System.Net.Mime.MediaTypeNames;
using HotelManagement.RequestModels.RoomRequestModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelManagement.Services
{
    public class RoomService : IRoomService
    {
        private IGenericRepository<Room> genericRepository;

        public RoomService(IGenericRepository<Room> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public IEnumerable<GetRoomResponse> GetAllRooms()
        {
            try
            {
                var rooms = genericRepository.GetAll().AsNoTracking().Include(x => x.RoomImages).ToList();
                List<string> images = new List<string>();
                List<GetRoomResponse> response = new List<GetRoomResponse>();
                foreach (var room in rooms)
                {
                    response.Add(new GetRoomResponse
                    {
                        Id = room.Id,
                        RoomNumber = room.RoomNumber,
                        MaxPersons = room.MaxPersons,
                        Status = room.Status,
                        RoomImages = images
                    });

                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertRoom(Room model)
        {
            genericRepository.Insert(model);
            genericRepository.Save();
        }

        public void UpdateRoom(Room model)
        {
            var room = genericRepository.Query(x => x.Id == model.Id).FirstOrDefault();
            if (room is null)
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            genericRepository.Update(model);
            genericRepository.Save();
            
        }
        public void DeleteRoom(int id)
        {
            genericRepository.Delete(id);
            genericRepository.Save();
        }
        public GetRoomResponse GetRoomById(int id)
        {
            var room = genericRepository.Query(x => x.Id == id).AsNoTracking().Include(x=>x.RoomImages).FirstOrDefault();
            if (room is null)
                return new GetRoomResponse();
            List<string> images = new List<string>();
            room.RoomImages.ToList().ForEach(x => { images.Add(x.ImagePath); });
            GetRoomResponse rooms = new GetRoomResponse
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                MaxPersons = room.MaxPersons,
                Status = room.Status,
                RoomImages = images
            };

            return rooms;
        }

        public IEnumerable<GetRoomResponse> GetRoomByStatus(RoomStatus status)
        {
            var rooms = genericRepository.GetAll().Where(x => x.Status == (short)status).AsNoTracking().Include(x => x.RoomImages).ToList();
            if (rooms is null)
            {
                return null;
            }
            List<string> images = new List<string>();
            List<GetRoomResponse> response = new List<GetRoomResponse>();
            foreach (var room in rooms)
            {
                response.Add(new GetRoomResponse
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    MaxPersons = room.MaxPersons,
                    Status = room.Status,
                    RoomImages = images
                });
            }
            return response;
        }
    }
}
