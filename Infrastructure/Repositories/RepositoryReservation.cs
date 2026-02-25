namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using Npgsql;
using DotNet_Console_Hotel.Models;
using System;
using System.Collections.Generic;

internal class RepositoryReservation
{
    public RepositoryReservation(string connection)
    {
        connectionString = connection;
    }

    private string connectionString;
    public List<Reservation> Reservations = new(); // Change by database implementation

    public void AddReservation(Reservation reservation)
    {
        Reservations.Add(reservation);
        ShowAllReservation();
    }

    public void ShowAllReservation()
    {
        Console.WriteLine("*** Showing All the reservations ***");
        foreach (var reservation in Reservations)
        {
            Console.WriteLine($"Reservation: UserId={reservation.ClientId}, RoomId={reservation.RoomId}, CheckInDate={reservation.CheckInDate:yyyy-MM-dd}, DataSaida={reservation.CheckOutDate:yyyy-MM-dd}");
        }
    }
}
