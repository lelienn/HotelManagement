using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class Booking
{
    public int Id { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public decimal Price { get; set; }

    public DateTime BookTime { get; set; }

    public int GuestId { get; set; }

    public int RoomId { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
