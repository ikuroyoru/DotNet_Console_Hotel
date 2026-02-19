namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo gerenciamento da sessão do usuário na aplicação.
/// </summary>
/// <remarks>
/// Este serviço mantém o estado do cliente atualmente logado.
/// Não há persistência de sessão em banco de dados ou arquivo;
/// o estado é mantido apenas enquanto a aplicação estiver em execução.
/// </remarks>
internal class SessaoService
{
    /// <summary>
    /// Identificador do cliente atualmente logado (CPF).
    /// </summary>
    /// <remarks>
    /// Valor padrão é "Desconectado".
    /// </remarks>
    public string IdCliente { get; private set; } = "Desconectado";
    public bool IsLoggedIn { get; private set; }

    /// <summary>
    /// Inicializa uma nova instância de <see cref="SessaoService"/>.
    /// </summary>
    public SessaoService() { }

    /// <summary>
    /// Inicia a sessão do cliente informado.
    /// </summary>
    /// <param name="idCliente">Identificador (CPF) do cliente.</param>
    /// <remarks>
    /// Atualiza o valor de <see cref="IdCliente"/> e exibe mensagem no console.
    /// </remarks>
    public void IniciarSessao(string idCliente)
    {
        IdCliente = idCliente;
        Console.WriteLine($"Sessão iniciada para o usuário: {IdCliente}");
        IsLoggedIn = true;
    }

    public void EncerrarSessao(string idCliente)
    {
        IdCliente = "Desconectado";
        Console.WriteLine($"Sessão encerrada para o usuário: {idCliente}");
        IsLoggedIn = false;
    }

    /// <summary>
    /// Retorna o identificador do cliente atualmente logado.
    /// </summary>
    /// <returns>
    /// CPF do cliente logado ou "Desconectado" se ninguém estiver logado.
    /// </returns>
    public (bool status, string userId) ObterUsuarioLogado()
    {
        if (IsLoggedIn)
        {
            return (true, IdCliente);
        }
        else return (false, "Usuario Nao Conectado");

    }
}
