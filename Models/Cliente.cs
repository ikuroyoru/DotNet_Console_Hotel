namespace DotNet_Console_Hotel.Models;

/// <summary>
/// Representa um cliente do hotel.
/// </summary>
/// <remarks>
/// Esta classe armazena informações básicas do cliente, como nome, CPF e senha,
/// além de manter uma lista de reservas associadas ao cliente.
/// 
/// Observações:
/// - A senha pode ser alterada internamente, mas não publicamente.
/// - A lista de reservas é gerenciada internamente e pode ser adicionada
///   usando o método <see cref="AdicionarReserva"/>.
/// - Não há lógica de autenticação nesta classe; apenas armazena os dados.
/// </remarks>
internal class Cliente
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Cliente"/>.
    /// </summary>
    /// <param name="nome">Nome completo do cliente.</param>
    /// <param name="cpf">CPF do cliente.</param>
    /// <param name="senha">Senha do cliente.</param>
    public Cliente(string nome, string cpf, string senha)
    {
        Nome = nome;
        Cpf = cpf;
        Senha = senha;
    }

    /// <summary>
    /// Nome completo do cliente.
    /// </summary>
    public string Nome { get; }

    /// <summary>
    /// CPF do cliente.
    /// </summary>
    public string Cpf { get; }

    /// <summary>
    /// Senha do cliente.
    /// </summary>
    /// <remarks>
    /// Pode ser alterada apenas internamente.
    /// </remarks>
    public string Senha { get; private set; }

    /// <summary>
    /// Lista de reservas associadas a este cliente.
    /// </summary>
    public List<Reserva> Reservas { get; } = new();

    /// <summary>
    /// Adiciona uma reserva à lista de reservas do cliente.
    /// </summary>
    /// <param name="reserva">Reserva a ser adicionada.</param>
    public void AdicionarReserva(Reserva reserva)
    {
        Reservas.Add(reserva);

        foreach (var r in Reservas)
        {
            Console.WriteLine($"Reserva adicionada para o cliente {Nome}: {r.NomeHotel} - Quarto {r.NumeroQuarto} de {r.DataEntrada:yyyy-MM-dd} a {r.DataSaida:yyyy-MM-dd}");
        }
    }
}
