using System;

public class Reservation
{
    public int ReservationId { get; set; }
    public DataTime DateFrom { get; set; }
    public DataTime DateTo { get; set; }
    public Guest Guest { get; set; }
    public Room Room { get; set; }
    public decimal Price { get; set; }

}
