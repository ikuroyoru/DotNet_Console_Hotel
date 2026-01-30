/// <summary>
/// Representa uma reserva associada a um quarto.
/// </summary>
/// <remarks>
/// A reserva contém apenas o período de estadia,
/// definido por data de entrada e data de saída.
/// 
/// Não há validação de datas nesta classe.
/// Não há controle de status (ativo, cancelado, reembolsado).
/// Não há associação com cliente.
/// 
/// A responsabilidade de validar conflito de datas
/// é externa a esta classe.
/// </remarks>
internal class Reserva
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Reserva"/>.
    /// </summary>
    /// <param name="dataEntrada">Data de entrada da reserva.</param>
    /// <param name="dataSaida">Data de saída da reserva.</param>
    public Reserva(DateTime dataEntrada, DateTime dataSaida)
    {
        DataEntrada = dataEntrada;
        DataSaida = dataSaida;
    }

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
