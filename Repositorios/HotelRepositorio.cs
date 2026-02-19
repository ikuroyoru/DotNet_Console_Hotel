namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

internal class HotelRepositorio
{
    public HotelRepositorio(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    NpgsqlConnection _connection;
    public List<Hotel> Hoteis = new(); // TROCAR PELO BANCO DE DADOS

    public void Adicionar(Hotel hotel)
    {
        Hoteis.Add(hotel);
    }

    public IReadOnlyList<Hotel> ObterTodos()
    {
        return Hoteis.AsReadOnly();
    }
}
