namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using DotNet_Console_Hotel.Models;
using Npgsql;

internal class RepositoryClient // Qualquer operacao que tenha relacao direta com o banco de dados e a entidade Cliente e feita aqui.
{   
    public RepositoryClient(string connection)
    {
        connectionString = connection;
    }

    private string connectionString;

    public bool VerifyClientWithEmail(string email)
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

    public void NewClient(string name, string email, string password) 
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO cliente (nome, email, senha_hash) VALUES (@nome, @email, @senha)", connection);

        command.Parameters.AddWithValue("@nome", name);
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@senha", password);
        command.ExecuteNonQuery();
    }

    public Client? GetClientByAuthentication(string email, string senha)
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
            return new Client(clienteId, nome, emailCliente);
        }
        else
           return null;
    }

    public Client? GetClientById(Guid? id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, email FROM cliente WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id!);

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            Guid? clientId = reader.GetGuid(0);
            string name = reader.GetString(1);
            string clientEmail = reader.GetString(2);
            return new Client(clientId, name, clientEmail);
        }
        else
           return null;
    }
}
