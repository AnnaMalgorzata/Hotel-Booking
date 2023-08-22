namespace HotelBooking.BusinessLogic.Dtos;
public class BasicRoomInfo
{
    public string RoomType { get; set; }
    public int Capacity { get; set; }

    public BasicRoomInfo(string RoomType, int Capacity) 
    {
        this.RoomType = RoomType;
        this.Capacity = Capacity;
    }
}
