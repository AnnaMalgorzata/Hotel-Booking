namespace HotelBooking.BusinessLogic.Dtos;
public class BasicRoomInfo
{
    public RoomType Type { get; set; }
    public int Capacity { get; set; }

    public BasicRoomInfo(RoomType Type, int Capacity) 
    {
        this.Type = Type;
        this.Capacity = Capacity;
    }
}
