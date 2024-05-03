using System;
using System.Collections.Generic;

namespace HotelManagement.Models;

public partial class RoomImage
{
    public int Id { get; set; }

    public string ImagePath { get; set; } = null!;

    public int RoomId { get; set; }

    public virtual Room Room { get; set; } = null!;
}
