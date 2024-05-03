using HotelManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using HotelManagement.Repository;
using HotelManagement.Interfaces;
using HotelManagement.ResponseModels.BookingResponseModels;
using HotelManagement.ResponseModels.GuestResponseModels;
using HotelManagement.ResponseModels.RoomResponseModels;
using HotelManagement.RequestModels.GuestRequestModels;
using System.Net;

namespace HotelManagement.Services
{
    public class GuestService : IGuestService
    {
        private IGenericRepository<Guest> genericRepository;

        public GuestService(IGenericRepository<Guest> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        public IEnumerable<GetGuestResponse> GetAllGuests()
        {
            try
            {
                var guests = genericRepository.GetAll().AsNoTracking().ToList();

                List<GetGuestResponse> response = new List<GetGuestResponse>();
                foreach (var guest in guests)
                {
                    response.Add(new GetGuestResponse
                    {
                        Id = guest.Id,
                        FirstName = guest.FirstName,
                        LastName = guest.LastName,
                        DateOfBirth = guest.DateOfBirth,
                        Address = guest.Address,
                        Phone = guest.Phone,
                        Email = guest.Email,
                        Username = guest.Username
                    });

                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertGuest(Guest model)
        {
            genericRepository.Insert(model);
            genericRepository.Save();
        }

        public void UpdateGuest(Guest model)
        {
            var guest = genericRepository.Query(x => x.Id == model.Id).AsNoTracking().FirstOrDefault();
            if (guest is null)
            {
                throw new Exception(HttpStatusCode.BadRequest.ToString());
            }

            genericRepository.Update(model);
            genericRepository.Save();
        }
        public void DeleteGuest(int id)
        {
            genericRepository.Delete(id);
            genericRepository.Save();
        }
        public GetGuestResponse GetGuestById(int id)
        {
            var guest = genericRepository.Query(x => x.Id == id).AsNoTracking().FirstOrDefault();
            if (guest is null)
                return new GetGuestResponse();

            GetGuestResponse guests = new GetGuestResponse
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                DateOfBirth = guest.DateOfBirth,
                Address = guest.Address,
                Phone = guest.Phone,
                Email = guest.Email,
                Username= guest.Username
            };

            return guests;
        }
    }
}

