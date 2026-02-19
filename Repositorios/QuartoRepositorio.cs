namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using System;
using DotNet_Console_Hotel.Models;

internal class QuartoRepositorio
{
    public QuartoRepositorio(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    NpgsqlConnection _connection; 
    public List<Quarto> Quartos = new(); // TROCAR PELO BANCO DE DADOS

    public void Adicionar(Quarto quarto)
    {
        Quartos.Add(quarto);
    }

    public IReadOnlyList<Quarto> ObterTodos()
    {
        return Quartos.AsReadOnly();
    }

    public Quarto? ObterPorNumero(string hotelId, int numero)
    {
        return Quartos.Find(q => q.id == numero && q.HotelId == hotelId);
    }
}
