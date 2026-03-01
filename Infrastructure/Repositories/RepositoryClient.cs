namespace DotNet_Console_Hotel.Infrastructure.Repositories;

using DotNet_Console_Hotel.Core.Common;
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
        using var context = new HotelBookerContext(connectionString);

        var client = context.Clients.FirstOrDefault(c => c.Email == email);

        if (client == null) return false;
        return true;
    }

    public void NewClient(string name, string email, string password)
    {
        using var context = new HotelBookerContext(connectionString);

        var client = new Client(name, email, password);

        context.Clients.Add(client);
        context.SaveChanges();
    }

    public Client? GetClientByAuthentication(string email, string password)
    {
        using var context = new HotelBookerContext(connectionString);

        var client = context.Clients
            .FirstOrDefault(c =>
                c.Email == email &&
                c.Password == password);

        return client; // Null return if don't find
    }

    public Client? GetClientById(Guid? id)
    {
        using var context = new HotelBookerContext(connectionString);

        var client = context.Clients
            .FirstOrDefault(c =>
                c.Id == id);

        return client; // Null return if don't find
    }
}
