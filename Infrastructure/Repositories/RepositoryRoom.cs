namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;
using System.ComponentModel.DataAnnotations;

internal class RepositoryRoom
{
    public RepositoryRoom(string connection)
    {
        connectionString = connection;
    }

    string connectionString;

    public void AddGeneratedRooms(List<Room> rooms)
    {
        using var context = new HotelBookerContext(connectionString);

        foreach (var room in rooms)
        {
            context.Rooms.Add(room);
        }

        context.SaveChanges();
    }

    /// <summary>
    /// Retrieves the first available room for the specified hotel and date range.
    /// A room is considered available when it has no existing reservations
    /// that conflict with the provided check-in and check-out dates.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <param name="checkInDate">The desired check-in date (UTC).</param>
    /// <param name="checkOutDate">The desired check-out date (UTC).</param>
    /// <returns>
    /// The first available <see cref="Room"/> ordered by room number,
    /// or null if no rooms are available for the selected period.
    /// </returns>
    public Room? GetAvailableRoom(Guid hotelId, DateTime checkInDate, DateTime checkOutDate)
    {
        using var context = new HotelBookerContext(connectionString);

        return context.Rooms
            .Where(r => r.Hotel_Id == hotelId)
            .Where(r => !context.Reservations.Any(res =>
                res.Room_Id == r.Id &&
                checkInDate < res.CheckOut_Date &&
                checkOutDate > res.CheckIn_Date))
            .OrderBy(r => r.Number) // opcional, mas deixa previsível
            .FirstOrDefault();
    }
}
