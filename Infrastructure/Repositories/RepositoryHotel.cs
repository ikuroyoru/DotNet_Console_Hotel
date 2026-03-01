using DotNet_Console_Hotel.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DotNet_Console_Hotel.Infrastructure.Repositories;

internal class RepositoryHotel
{
    public RepositoryHotel(string connection)
    {
        connectionString = connection;
    }

    string connectionString;

    public Hotel? AddHotel(Hotel hotel)
    {
        using var context = new HotelBookerContext(connectionString);

        // Verify if exist an hotel with the same data
        var exists = context.Hotels.Any(h =>
            h.Name == hotel.Name &&
            h.Street == hotel.Street &&
            h.Number == hotel.Number &&
            h.Zip_code == hotel.Zip_code
        );

        if (exists) return null; // already exist

        context.Hotels.Add(hotel);
        context.SaveChanges();

        return hotel;
    }

    public IReadOnlyList<Hotel> GetHotels(int loadQtd)
    {
        using var context = new HotelBookerContext(connectionString);

        var hotels = context.Hotels.Take(loadQtd).ToList();

        return hotels;
    }


}
