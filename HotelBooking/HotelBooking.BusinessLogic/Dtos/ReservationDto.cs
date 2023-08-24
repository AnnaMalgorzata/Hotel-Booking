namespace HotelBooking.BusinessLogic.Dtos;
public class ReservationDto
{
    public int Id { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal Price { get; set; }
    public string GuestFirstname { get; set; }
    public string GuestLastname { get; set; }
    public ICollection<BasicRoomInfo> RoomInfos { get; set; }

}
