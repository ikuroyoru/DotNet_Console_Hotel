using DotNet_Console_Hotel.Repositorios;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo gerenciamento de clientes.
/// </summary>
/// <remarks>
/// Esta classe atua como camada de serviço entre a aplicação e o <see cref="ClienteRepositorio"/>.
/// Fornece métodos para:
/// - Criar novos clientes.
/// - Buscar clientes pelo CPF.
/// - Adicionar reservas aos clientes existentes.
/// </remarks>
internal class ClienteService
{
    private readonly ClienteRepositorio _clienteRepositorio;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ClienteService"/>.
    /// </summary>
    /// <param name="clienteRepositorio">Repositório utilizado para armazenar e consultar clientes.</param>
    public ClienteService(ClienteRepositorio clienteRepositorio)
    {
        _clienteRepositorio = clienteRepositorio;
    }

    // Adiciona cliente ao sistema
    public void NovoCliente(string nome, string cpf, string senha)
    {
        var cliente = new Cliente(nome, cpf, senha);
        _clienteRepositorio.Adicionar(cliente);
    }

    // Busca cliente pelo id/cpf
    public Cliente? BuscarCliente(string cpf)
    {
        return _clienteRepositorio.BuscarPorCpf(cpf);
    }
}
