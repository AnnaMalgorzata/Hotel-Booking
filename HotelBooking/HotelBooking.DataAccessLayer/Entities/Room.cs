using System;

public class Room
{
    public int Number { get; set; }
    public int Floor { get; set; }
    public RoomType Type { get; set; }
    public int Capacity { get; set; }
    public int PricePerNight { get; set; }

    //pokój może być rezerwowany przez wiele osób
    //może mieć wiele rezerwacji
}
