using DotNet_Console_Hotel.Core.Common;

namespace DotNet_Console_Hotel.Services;

internal class ServiceSession
{
    public Guid ClientId { get; private set; }
    public bool IsLoggedIn { get; private set; }

    public Result SessionStart(Guid clientId)
    {
        if (clientId == Guid.Empty)
        {
            return Result.Fail("Error: Invalid Client ID");
        }

        this.ClientId = clientId;
        IsLoggedIn = true;

        return Result.Ok();
    }

    public void EndSession(Guid clientId)
    {
        ClientId = Guid.Empty;
        IsLoggedIn = false;
        Console.WriteLine($"End Session for the user: {clientId}");
    }

    public Guid GetConnectedUserId()
    {
        if (IsLoggedIn)
            return ClientId;
        else 
            return Guid.Empty;
    }
}
