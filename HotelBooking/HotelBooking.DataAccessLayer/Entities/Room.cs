﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Room
{
    public int RoomId { get; set; }
    public int Number { get; set; }
    public int Floor { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
}
