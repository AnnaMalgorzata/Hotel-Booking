namespace HotelBooking.BusinessLogic.Dtos;
public class BasicRoomInfo
{
    public RoomType Type { get; set; }
    public int Capacity { get; set; }

    public BasicRoomInfo(RoomType type, int capacity) 
    {
        Type = type;
        Capacity = capacity;
    }
}
