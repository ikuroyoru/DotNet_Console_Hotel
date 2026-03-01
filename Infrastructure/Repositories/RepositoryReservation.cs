namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using DotNet_Console_Hotel.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;

internal class RepositoryReservation
{
    public RepositoryReservation(string connection)
    {
        connectionString = connection;
    }

    private string connectionString;

    public void AddReservation(Reservation reservation)
    {
        using var context = new HotelBookerContext(connectionString);

        context.Reservations.Add(reservation);
        context.SaveChanges();
    }

    public void RemoveReservation() { } // Delete a reservation
    public void ShowMyReservations() { } // Get All user's reservations
    public void UpdateReservation() { } // Update reservation's data, like checkin and checkout date
                                        // Check-in and check-out dates can be changed only if they are more than one week from the current date.

    // COMMIT QUE IMPLEMENTA OS 3 METODOS ACIMA
}
