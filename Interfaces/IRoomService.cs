using HotelManagement.Models;
using HotelManagement.Models.Constants;
using HotelManagement.RequestModels.RoomRequestModels;
using HotelManagement.ResponseModels.BookingResponseModels;
using HotelManagement.ResponseModels.RoomResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<GetRoomResponse> GetAllRooms();
        GetRoomResponse GetRoomById(int id);
        void InsertRoom(Room obj);
        void UpdateRoom(Room model);
        void DeleteRoom(int id);
        IEnumerable<GetRoomResponse> GetRoomByStatus(RoomStatus status);
    }
}
