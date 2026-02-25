using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

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
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var comm = new NpgsqlCommand(
                @"INSERT INTO hotel 
                (nome, rua, numero, cidade, estado, cep, pais, telefone, quantidade_quartos, ativo)
                VALUES 
                (@nome, @rua, @numero, @cidade, @estado, @cep, @pais, @telefone, @quantidade_quartos, @ativo)
                ON CONFLICT (nome, rua, numero, cep)
                DO NOTHING
                RETURNING id;",
                connection);

        comm.Parameters.AddWithValue("@nome", hotel.Name);
        comm.Parameters.AddWithValue("@rua", hotel.Street);
        comm.Parameters.AddWithValue("@numero", hotel.Number);
        comm.Parameters.AddWithValue("@cidade", hotel.City);
        comm.Parameters.AddWithValue("@estado", hotel.State);
        comm.Parameters.AddWithValue("@cep", hotel.Zipcode);
        comm.Parameters.AddWithValue("@pais", hotel.Country);
        comm.Parameters.AddWithValue("@telefone", hotel.Telephone);
        comm.Parameters.AddWithValue("@quantidade_quartos", hotel.RoomCount);
        comm.Parameters.AddWithValue("@ativo", hotel.Active);


        Guid? hotelId = comm.ExecuteScalar() as Guid?; // Return database's generated ID or null if the hotel already exists (due to ON CONFLICT DO NOTHING)

        if (hotelId.HasValue)
        {
            hotel.Id = hotelId.Value;
                               
            return hotel;
        }
        else
        {
            return null;
        }

        
    }

    public IReadOnlyList<Hotel> LoadHotels(int loadQtd)
    {
        return new List<Hotel>().AsReadOnly(); // RETORNA LISTA VAZIA - EM ALTERACAO PARA USAR O BANCO
    }

    
}
