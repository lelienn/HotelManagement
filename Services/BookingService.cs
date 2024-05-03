using HotelManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Interfaces;
using HotelManagement.ResponseModels.BookingResponseModels;
using System.Net;
using HotelManagement.ResponseModels.RoomResponseModels;
using HotelManagement.Models.Constants;

namespace HotelManagement.Services
{
    public class BookingService : IBookingService
    {
        private readonly IGenericRepository<Booking> bookingGenericRepository;
        private readonly IGenericRepository<Room> roomGenericRepository;

        public BookingService(IGenericRepository<Booking> bookingGenericRepository, IGenericRepository<Room> roomGenericRepository)
        {
            this.bookingGenericRepository = bookingGenericRepository;
            this.roomGenericRepository = roomGenericRepository;
        }
        public IEnumerable<GetBookingResponse> GetAllBookings()
        {
            try
            {
                var bookings = bookingGenericRepository.GetAll().AsNoTracking().Include(x => x.Guest).Include(x => x.Room).ToList();

                List<GetBookingResponse> response = new List<GetBookingResponse>();
                foreach (var booking in bookings)
                {
                    response.Add(new GetBookingResponse
                    {
                        Id = booking.Id,
                        CheckInDate = booking.CheckInDate,
                        CheckOutDate = booking.CheckOutDate,
                        Price = booking.Price,
                        GuestId = (int)booking.GuestId,
                        RoomId = (int)booking.RoomId,
                        Guest = new GetBookingWithGuestResponse
                        {
                            Id = booking.Guest.Id,
                            FirstName = booking.Guest.FirstName,
                            LastName = booking.Guest.LastName,
                            Phone = booking.Guest.Phone,
                            Email = booking.Guest.Email,
                        },
                        Room = new GetBookingWithRoomResponse
                        {
                            Id = booking.Room.Id,
                            RoomNumber = booking.Room.RoomNumber,
                            Status = booking.Room.Status,
                        }
                    });

                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertBooking(Booking model)
        {
            var room = roomGenericRepository.Query(x => x.Id == model.RoomId).FirstOrDefault();
            if (room is null && room.Status == (short)RoomStatus.Passive)
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }
           
            bookingGenericRepository.Insert(model);
            bookingGenericRepository.Save();
            room.Status = (short)RoomStatus.Passive;
            roomGenericRepository.Update(room);
            roomGenericRepository.Save();

        }

        public void UpdateBooking(Booking model)
        {
            var booking = bookingGenericRepository.Query(x => x.Id == model.Id).FirstOrDefault();
            if (booking is null)
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            bookingGenericRepository.Update(model);
            bookingGenericRepository.Save();
        }
        public void DeleteBooking(int id)
        {
            bookingGenericRepository.Delete(id);
            bookingGenericRepository.Save();
        }
        public GetBookingResponse GetBookingById(int id)
        {
            var booking = bookingGenericRepository.Query(x => x.Id == id).AsNoTracking()
                .Include(y => y.Guest)
                .Include(y => y.Room).FirstOrDefault();
            if (booking is null)
                return new GetBookingResponse();

            GetBookingResponse bookings = new GetBookingResponse
            {
                Id = booking.Id,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                Price = booking.Price,
                GuestId = (int)booking.GuestId,
                RoomId = (int)booking.RoomId,
                Guest = new GetBookingWithGuestResponse
                {
                    Id = booking.Guest.Id,
                    FirstName = booking.Guest.FirstName,
                    LastName = booking.Guest.LastName,
                    Phone = booking.Guest.Phone,
                    Email = booking.Guest.Email,
                },
                Room = new GetBookingWithRoomResponse
                {
                    Id = booking.Room.Id,
                    RoomNumber = booking.Room.RoomNumber,
                    Status = booking.Room.Status,
                }
            };

            return bookings;
        }

    }
}