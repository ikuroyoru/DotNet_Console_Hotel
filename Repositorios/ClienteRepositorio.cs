namespace DotNet_Console_Hotel.Repositorios;

using DotNet_Console_Hotel.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Security.Cryptography;

internal class ClienteRepositorio // Qualquer operacao que tenha relacao direta com o banco de dados e a entidade Cliente e feita aqui.
{   
    public ClienteRepositorio(string connection)
    {
        connectionString = connection;
    }

    private string connectionString;

    public bool VerificaClienteComEmail(string email)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("SELECT nome FROM cliente WHERE email = @email", connection);
        command.Parameters.AddWithValue("@email", email);

        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NovoCliente(string nome, string email, string senha) 
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO cliente (nome, email, senha_hash) VALUES (@nome, @email, @senha)", connection);

        command.Parameters.AddWithValue("@nome", nome);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@senha", senha);
        command.ExecuteNonQuery();
    }

    public Cliente? BuscarClienteAutenticacao(string email, string senha)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, email FROM cliente WHERE email = @email AND senha_hash = @senha", connection);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@senha", senha);

        var reader = command.ExecuteReader();

        if (reader.Read())
        {
            Guid? clienteId = reader.GetGuid(0);
            string nome = reader.GetString(1);
            string emailCliente = reader.GetString(2);
            return new Cliente(clienteId, nome, emailCliente);
        }
        else
           return null;
    }

    public Cliente? BuscarClientePorId(Guid? id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, email FROM cliente WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id!);

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Guid? clienteId = reader.GetGuid(0);
            string nome = reader.GetString(1);
            string emailCliente = reader.GetString(2);
            return new Cliente(clienteId, nome, emailCliente);
        }
        else
           return null;
    }
}
