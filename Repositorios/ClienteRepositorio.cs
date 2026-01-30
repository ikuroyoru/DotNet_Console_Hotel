namespace DotNet_Console_Hotel.Repositorios;

using DotNet_Console_Hotel.Models;

/// <summary>
/// Repositório responsável por armazenar clientes do sistema.
/// </summary>
/// <remarks>
/// Esta classe mantém uma lista interna de clientes e fornece métodos
/// para consulta e adição de contas.  
/// Não realiza persistência em banco de dados; todos os dados ficam em memória.
/// </remarks>
internal class ClienteRepositorio
{
    private readonly List<Cliente> _contas = new();

    /// <summary>
    /// Busca um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente a ser buscado.</param>
    /// <returns>
    /// A instância de <see cref="Cliente"/> correspondente ao CPF,
    /// ou null se não encontrado.
    /// </returns>
    public Cliente? BuscarPorCpf(string cpf)
    {
        return _contas.FirstOrDefault(c => c.Cpf == cpf);
    }

    /// <summary>
    /// Adiciona um novo cliente ao repositório.
    /// </summary>
    /// <param name="conta">Cliente a ser adicionado.</param>
    public void Adicionar(Cliente conta)
    {
        _contas.Add(conta);
    }
}
