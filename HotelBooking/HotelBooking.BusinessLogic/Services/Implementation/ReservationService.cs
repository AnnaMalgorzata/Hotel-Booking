using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class ReservationService
{
    //metoda typu GetReservation(int id), która zwraca nasz obiekt Dto,
    //czyli serwis co powinien zrobić to pobrać używając klasy repozytorium
    //rezerwację o podanym Id, zmapować obiekt rezerwacji na Dto (nie chcemy
    //zwracać obiektu modelowego) i zwrócić go

    private IReservationRepository _iReservationRepository;

    public ReservationService(IReservationRepository iReservationRepository)
    {
        _iReservationRepository = iReservationRepository;
    }

    public async Task<ReservationDto> GetReservation(int id)
    {
        var reservation = await _iReservationRepository.Get(id);

        /*if (reservation == null)
        {
            return NotFound();
        }*/

        var dto = new ReservationDto()
        {
            Id = reservation.ReservationId,
            DateFrom = reservation.DateFrom,
            DateTo = reservation.DateTo,
            Price = reservation.Price,
            GuestFirstname = reservation.Guest.Firstname, //nie zadziała, dopisać dedykowaną metodę w res..Repository
            GuestLastname = reservation.Guest.Lastname,
            Rooms = reservation.Rooms
        };       

        return dto;
    }
}
