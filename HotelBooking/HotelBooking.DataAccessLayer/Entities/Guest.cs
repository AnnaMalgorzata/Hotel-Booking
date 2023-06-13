using System;

public class Guest
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public long PhoneNumber { get; set; }
    private int age; //data urodzenia ewentualnie
    public int Age
    {
        get { return age; }
        //żeby mieć zapisany przykład pełnej właściwości 
        set 
        {
            if (value >= 18 && value < 120)
                age = value;
            else
                Console.WriteLine("Ask someone in the age range of 18 to 120 to make the reservation.");
        }
    }

    //klient może zarezerwoać wiele pokojów
    //może zrobić wiele rejestracji
}
