using DotNet_Console_Hotel.Core.Common;
using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Models;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

internal class ServiceReservation
{
    private readonly ServiceSession _serviceSession;
    private readonly RepositoryReservation _repositoryReservation;
    private readonly RepositoryRoom _repositoryRoom;

    public ServiceReservation(ServiceSession serviceSession, RepositoryReservation repositoryReservation, RepositoryRoom repositoryRoom)
    {
        _repositoryRoom = repositoryRoom;
        _serviceSession = serviceSession;
        _repositoryReservation = repositoryReservation;
    }

    /// <summary>
    /// Validates business rules and attempts to create a reservation for the specified hotel.
    /// The reservation is created only if the user is connected, the dates are valid,
    /// and at least one room is available without date conflicts.
    /// </summary>
    /// <param name="hotel">The hotel where the reservation will be made.</param>
    /// <param name="checkInDate">The desired check-in date.</param>
    /// <param name="checkoutDate">The desired check-out date.</param>
    /// <returns>
    /// Returns <see cref="Result.Ok"/> if the reservation is successfully created;
    /// otherwise, returns <see cref="Result.Fail"/> with an error message.
    /// </returns>
    public Result CreateReservation(Hotel hotel, DateTime checkInDate, DateTime checkoutDate)
    {
        Guid clientId = _serviceSession.GetConnectedUserId();

        // Return avaliable room with validated checkIn and checkOut dates
        Room? room = _repositoryRoom.GetAvailableRoom(hotel.Id, checkInDate, checkoutDate);

        if (clientId == Guid.Empty)
            return Result.Fail("Usuário não conectado.");

        if (hotel == null)
            return Result.Fail("Hotel inválido.");

        if (room == null)
            return Result.Fail("O hotel não possui quartos disponíveis.");

        var reservation = new Reservation(clientId, room.Id, checkInDate, checkoutDate);

        _repositoryReservation.AddReservation(reservation);

        return Result.Ok();
    }

}
