namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

internal class HotelRepositorio
{
    public HotelRepositorio(string connection)
    {
        connectionString = connection;
    }

    string connectionString;

    // LOGICA PARA INCLUIR NO BANCO DE DADOS OS HOTEIS GERADOS A PARTIR DO CSV LIDO NA CLASSE csvReader
    public Hotel? AdicionarHoteis(Hotel hotel)
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

        comm.Parameters.AddWithValue("@nome", hotel.Nome);
        comm.Parameters.AddWithValue("@rua", hotel.Rua);
        comm.Parameters.AddWithValue("@numero", hotel.Numero);
        comm.Parameters.AddWithValue("@cidade", hotel.Cidade);
        comm.Parameters.AddWithValue("@estado", hotel.Estado);
        comm.Parameters.AddWithValue("@cep", hotel.Cep);
        comm.Parameters.AddWithValue("@pais", hotel.Pais);
        comm.Parameters.AddWithValue("@telefone", hotel.Telefone);
        comm.Parameters.AddWithValue("@quantidade_quartos", hotel.QuantidadeQuartos);
        comm.Parameters.AddWithValue("@ativo", hotel.Ativo);


        Guid? hotelId = comm.ExecuteScalar() as Guid?; // RETORNA O ID GERADO PELO BANCO DE DADOS PARA O HOTEL INSERIDO

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

    public IReadOnlyList<Hotel> ObterTodos()
    {
        return new List<Hotel>().AsReadOnly(); // RETORNA LISTA VAZIA - EM ALTERACAO PARA USAR O BANCO
    }

    
}
