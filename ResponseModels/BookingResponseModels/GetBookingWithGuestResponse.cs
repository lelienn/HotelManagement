﻿namespace HotelManagement.ResponseModels.BookingResponseModels
{
    public class GetBookingWithGuestResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
