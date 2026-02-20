namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

internal class QuartoRepositorio
{
    public QuartoRepositorio(string connection)
    {
        connectionString = connection;
    }

    string connectionString;

    // LOGICA PARA INCLUIR NO BANCO DE DADOS OS QUARTOS GERADOS A PARTIR DO CSV LIDO NA CLASSE csvReader
    public void AdicionarQuartos(List<Quarto> quartos)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        NpgsqlCommand comm = new NpgsqlCommand(
            "INSERT INTO " +
            "quarto (numero, categoria, hotel_id, preco) " +
            "VALUES (@numero, @categoria, @hotel_id, @preco)", connection
            );

        foreach (var quarto in quartos)
        {
            comm.Parameters.Clear();
            comm.Parameters.AddWithValue("numero", quarto.Numero);
            comm.Parameters.AddWithValue("categoria", quarto.Categoria);
            comm.Parameters.AddWithValue("hotel_id", quarto.HotelId);
            comm.Parameters.AddWithValue("preco", quarto.Preco);
            comm.ExecuteNonQuery();
        }


        // LOGICA PARA INCLUIR OS QUARTOS GERADOS NO BANCO DE DADOS
    }
}
