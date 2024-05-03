using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Room
{
    public int Id { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int MaxPersons { get; set; }

    public short Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<RoomImage> RoomImages { get; set; } = new List<RoomImage>();
}
