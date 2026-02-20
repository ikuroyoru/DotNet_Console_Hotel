namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using DotNet_Console_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Text;


internal class ReservaRepositorio
{
    public ReservaRepositorio(string connection)
    {
        connectionString = connection;
    }

    string connectionString;
    public List<Reserva> Reservas = new(); // TROCAR PELO BANCO DE DADOS

    public void AdicionarReserva(Reserva reserva)
    {
        Reservas.Add(reserva);
        ExibirTodasAsReservas();
    }

    public void ExibirTodasAsReservas()
    {
        Console.WriteLine("*** EXIBINDO TODAS AS RESERVAS ***");
        foreach (var reserva in Reservas)
        {
            Console.WriteLine($"Reserva: UserId={reserva.UserId}, QuartoId={reserva.QuartoId}, DataEntrada={reserva.DataEntrada:yyyy-MM-dd}, DataSaida={reserva.DataSaida:yyyy-MM-dd}");
        }
    }
}
