using DotNet_Console_Hotel.Infrastructure.Repositories;

namespace DotNet_Console_Hotel.Services;

internal class ServiceRoom
{
    private readonly RepositoryRoom _repositoryRoom;

    public ServiceRoom(RepositoryRoom repositoryRoom)
    {
        _repositoryRoom = repositoryRoom;
    }


}
