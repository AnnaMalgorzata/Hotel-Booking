using HotelBooking.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BusinessLogic.Dtos;
public class ReservationDto
{
    public int Id { get; set; } //set?? chyba bez set
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public decimal Price { get; set; }
    public string GuestFirstname { get; set; }
    public string GuestLastname { get; set; }
    public ICollection<Room> Rooms { get; set; } //nie może być typu room, chce zwrócić typ pokoju i jego pojemność


}
