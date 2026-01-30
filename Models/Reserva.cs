namespace DotNet_Console_Hotel.Models;

/// <summary>
/// Representa uma reserva associada a um quarto e a um cliente.
/// </summary>
/// <remarks>
/// A reserva armazena apenas o período de estadia e referências via IDs:
/// - <see cref="NomeHotel"/>: nome do hotel reservado.
/// - <see cref="IdCliente"/>: identificador do cliente que fez a reserva.
/// - <see cref="NumeroQuarto"/>: número do quarto reservado.
///
/// Regras e responsabilidades:
/// - Não realiza validação de datas.
/// - Não gerencia status da reserva (ativo, cancelado, reembolsado).
/// - A verificação de conflito de datas deve ser feita externamente,
///   geralmente via <see cref="ReservaService"/>.
/// - A relação entre reserva, cliente e quarto é feita por referência de IDs e números,
///   não por objetos diretos.
/// </remarks>
internal class Reserva
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Reserva"/>.
    /// </summary>
    /// <param name="dataEntrada">Data de entrada da reserva.</param>
    /// <param name="dataSaida">Data de saída da reserva.</param>
    /// <param name="nomeHotel">Nome do hotel associado à reserva.</param>
    /// <param name="idCliente">ID do cliente que realizou a reserva.</param>
    /// <param name="numeroQuarto">Número do quarto reservado.</param>
    public Reserva(DateTime dataEntrada, DateTime dataSaida, string nomeHotel, string idCliente, int numeroQuarto)
    {
        DataEntrada = dataEntrada;
        DataSaida = dataSaida;
        NomeHotel = nomeHotel;
        IdCliente = idCliente;
        NumeroQuarto = numeroQuarto;
    }

    /// <summary>
    /// Nome do hotel associado à reserva.
    /// </summary>
    public string NomeHotel;

    /// <summary>
    /// Identificador do cliente que realizou a reserva.
    /// </summary>
    public string IdCliente;

    /// <summary>
    /// Número do quarto reservado.
    /// </summary>
    public int NumeroQuarto;

    /// <summary>
    /// Data de entrada da reserva.
    /// </summary>
    /// <remarks>
    /// Valor definido no momento da criação.
    /// Não pode ser alterado externamente.
    /// </remarks>
    public DateTime DataEntrada { get; private set; }

    /// <summary>
    /// Data de saída da reserva.
    /// </summary>
    /// <remarks>
    /// Valor definido no momento da criação.
    /// Não pode ser alterado externamente.
    /// </remarks>
    public DateTime DataSaida { get; private set; }
}
