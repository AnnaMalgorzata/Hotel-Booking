using System;

public class Reservation
{
    //cena końcowa = cena za pokój * ilość nocy
    //data początkowa
    //data końcowa
    public Guest MyGuest { get; set; }
    public Room MyRoom { get; set; }

    //wiele rejestracji do jednego pokoju?? || wiele do wielu??
    //rejestracja może być na jedną osobę, ale jedna osoba może mieć wiele rejestracji
}
