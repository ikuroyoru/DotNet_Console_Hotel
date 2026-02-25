using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Infrastructure.Repositories;

namespace DotNet_Console_Hotel.Services;

internal class ServiceClient
{
    private readonly RepositoryClient _clientRepository;

    public ServiceClient(RepositoryClient clientRepository)
    {
        _clientRepository = clientRepository;
    }
}
