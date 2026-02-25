using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Services;
namespace DotNet_Console_Hotel.Menus;


internal class MenuShowPersonalInformation : Menu
{
    public MenuShowPersonalInformation(RepositoryClient repositoryClient, ServiceSession serviceSession)
    {
        _repositoryClient = repositoryClient;
        _serviceSession = serviceSession;
    }

    private readonly RepositoryClient _repositoryClient;
    private readonly ServiceSession _serviceSession;

    public override void Execute()
    {
        var cliente = _repositoryClient.GetClientById(_serviceSession.GetConnectedUserId());

        if (cliente == null)
        {
            Console.WriteLine("Error: Client not found: Disconnected user.");
            return;
        }

        Console.WriteLine("***** My Personal Info *****\n");
        Console.WriteLine($"Id: {cliente.Id}");
        Console.WriteLine($"Name: {cliente.Name}");
        Console.WriteLine($"E-Mail: {cliente.Email}");
    }
}
