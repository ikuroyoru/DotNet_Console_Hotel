namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável por gerenciar reservas de quartos.
/// </summary>
/// <remarks>
/// Responsabilidade:
/// - Validar período informado.
/// - Identificar um quarto disponível dentro do hotel.
/// - Criar a reserva no primeiro quarto disponível encontrado.
/// 
/// O serviço não persiste dados diretamente.
/// A criação da reserva é delegada ao próprio objeto <see cref="Quarto"/>.
/// </remarks>
internal class ReservaService
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="ReservaService"/>.
    /// </summary>
    public ReservaService() { }

    /// <summary>
    /// Tenta atribuir um quarto disponível para o período informado.
    /// </summary>
    /// <param name="hotel">Hotel onde a reserva será realizada.</param>
    /// <param name="dataEntrada">Data de entrada do hóspede.</param>
    /// <param name="dataSaida">Data de saída do hóspede.</param>
    /// <returns>
    /// Uma tupla contendo:
    /// - mensagem: descrição do resultado da operação.
    /// - sucesso: indica se a reserva foi criada com êxito.
    /// </returns>
    /// <remarks>
    /// Regras aplicadas:
    /// - As datas não podem ser o valor padrão (DateTime default).
    /// - A data de saída deve ser posterior à data de entrada.
    /// - Um quarto é considerado disponível quando NÃO existe reserva
    ///   cujo período sobreponha o intervalo solicitado.
    /// 
    /// Lógica de sobreposição utilizada:
    /// Existe conflito quando:
    /// dataEntrada < reserva.DataSaida
    /// E
    /// dataSaida > reserva.DataEntrada
    /// 
    /// Caso não haja quarto disponível, a operação retorna falha.
    /// O primeiro quarto disponível encontrado é utilizado.
    /// </remarks>
    public (string mensagem, bool sucesso) AtribuirQuarto(Hotel hotel, DateTime dataEntrada, DateTime dataSaida)
    {
        if (dataEntrada == default || dataSaida == default)
        {
            return ("As datas nao podem ser vazias.", false);
        }

        if (dataSaida <= dataEntrada)
        {
            return ("A data de saida deve ser posterior à data de entrada.", false);
        }

        var quartoDisponivel = hotel.Quartos
        .FirstOrDefault(quarto =>
            !quarto.Reservas.Any(reserva =>
                dataEntrada < reserva.DataSaida &&
                dataSaida > reserva.DataEntrada
            )
        );

        if (quartoDisponivel == null)
        {
            return ($"Nao ha quartos disponiveis para o periodo {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}. Por favor, selecione outra data.", false);
        }

        quartoDisponivel.CriarReserva(dataEntrada, dataSaida);
        return ($"O quarto {quartoDisponivel.Numero} foi reservado com sucesso para {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}.", true);
    }
}
