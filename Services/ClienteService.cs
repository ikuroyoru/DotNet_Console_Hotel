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

    /// <summary>
    /// Adiciona um novo cliente ao sistema.
    /// </summary>
    /// <param name="nome">Nome completo do cliente.</param>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="senha">Senha escolhida pelo cliente.</param>
    public void AdicionarCliente(string nome, string cpf, string senha)
    {
        var cliente = new Cliente(nome, cpf, senha);
        _clienteRepositorio.Adicionar(cliente);
    }

    /// <summary>
    /// Adiciona uma reserva a um cliente existente.
    /// </summary>
    /// <param name="reserva">Reserva a ser associada ao cliente.</param>
    /// <exception cref="Exception">Lançada quando o cliente não é encontrado no repositório.</exception>
    public void AdicionarReserva(Reserva reserva, Cliente cliente)
    {
        cliente.AdicionarReserva(reserva);
    }

    /// <summary>
    /// Busca um cliente pelo CPF.
    /// </summary>
    /// <param name="cpf">CPF do cliente a ser buscado.</param>
    /// <returns>
    /// Instância de <see cref="Cliente"/> correspondente ao CPF,
    /// ou null se o cliente não for encontrado.
    /// </returns>
    public Cliente? BuscarCliente(string cpf)
    {
        return _clienteRepositorio.BuscarPorCpf(cpf);
    }
}
