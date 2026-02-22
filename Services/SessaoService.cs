namespace DotNet_Console_Hotel.Services;

internal class SessaoService
{
    public Guid? ClienteId { get; private set; }
    public bool IsLoggedIn { get; private set; }

    public Result IniciarSessao(Guid? clienteId)
    {
        if (clienteId == null || clienteId == Guid.Empty)
        {
            return Result.Fail("Erro: ID do cliente inválido para iniciar sessão.");
        }
        else
        {
            this.ClienteId = clienteId;
            IsLoggedIn = true;
            return Result.Ok();
        }
    }

    public void EncerrarSessao(Guid idCliente)
    {
        ClienteId = Guid.Empty;
        IsLoggedIn = false;
        Console.WriteLine($"Sessão encerrada para o usuário: {idCliente}");
    }

    public Guid? ObterIdUsuarioConectado()
    {
        if (IsLoggedIn)
            return ClienteId;
        else 
            return Guid.Empty;
    }
}
