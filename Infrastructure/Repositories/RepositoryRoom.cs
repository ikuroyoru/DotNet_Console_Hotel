namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

internal class RepositoryRoom
{
    public RepositoryRoom(string connection)
    {
        connectionString = connection;
    }

    string connectionString;

    public void AddRoom(List<Room> rooms)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand comm = new NpgsqlCommand(
            "INSERT INTO " +
            "quarto (numero, categoria, hotel_id, preco) " +
            "VALUES (@numero, @categoria, @hotel_id, @preco)", connection
            );

        foreach (var room in rooms)
        {
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("numero", room.Number);
            comm.Parameters.AddWithValue("categoria", room.Category);
            comm.Parameters.AddWithValue("hotel_id", room.HotelId);
            comm.Parameters.AddWithValue("preco", room.Price);
            comm.ExecuteNonQuery();
        }
    }
}
