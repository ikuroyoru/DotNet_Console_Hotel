using DotNet_Console_Hotel.Models;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo gerenciamento de reservas de quartos em hotéis.
/// </summary>
/// <remarks>
/// Atua como camada intermediária entre a aplicação e os serviços de clientes e quartos.
/// Principais responsabilidades:
/// - Validar período de reserva informado pelo usuário.
/// - Identificar um quarto disponível dentro do hotel.
/// - Criar a reserva no primeiro quarto disponível encontrado.
/// - Garantir consistência: a reserva é registrada tanto no cliente quanto no quarto.
///
/// A persistência dos dados é indireta, via <see cref="ClienteService"/> e <see cref="QuartoService"/>.
/// Este serviço não realiza persistência direta em banco de dados.
/// </remarks>
internal class ReservaService
{
    private readonly QuartoService _quartoService;
    private readonly ClienteService _clienteService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="ReservaService"/>.
    /// </summary>
    /// <param name="quartoService">Serviço responsável por gerenciar quartos e reservas associadas.</param>
    /// <param name="clienteService">Serviço responsável por gerenciar clientes e suas reservas.</param>
    public ReservaService(QuartoService quartoService, ClienteService clienteService)
    {
        _quartoService = quartoService;
        _clienteService = clienteService;
    }

    /// <summary>
    /// Tenta atribuir um quarto disponível para o período informado e criar uma reserva.
    /// </summary>
    /// <param name="hotel">Hotel onde a reserva será realizada.</param>
    /// <param name="clienteId">Identificador (CPF) do cliente que realizará a reserva.</param>
    /// <param name="dataEntrada">Data de entrada do hóspede.</param>
    /// <param name="dataSaida">Data de saída do hóspede.</param>
    /// <returns>
    /// Tupla contendo:
    /// - <c>mensagem</c>: descrição do resultado da operação.
    /// - <c>sucesso</c>: indica se a reserva foi criada com êxito.
    /// </returns>
    /// <remarks>
    /// Regras de validação:
    /// - As datas não podem ser vazias (<see cref="DateTime"/> default).
    /// - A data de saída deve ser posterior à data de entrada.
    /// 
    /// Lógica de verificação de disponibilidade:
    /// - Um quarto é considerado disponível quando não há nenhuma reserva existente que sobreponha o período solicitado.
    /// - A sobreposição é definida pela condição:
    ///   <c>dataEntrada < reserva.DataSaida && dataSaida > reserva.DataEntrada</c>
    /// 
    /// Fluxo de execução:
    /// 1. Verifica validade das datas.
    /// 2. Busca o primeiro quarto disponível no hotel.
    /// 3. Valida se o cliente existe.
    /// 4. Cria a instância de <see cref="Reserva"/>.
    /// 5. Adiciona a reserva ao cliente via <see cref="ClienteService.AdicionarReserva"/>.
    /// 6. Adiciona a reserva ao quarto via <see cref="QuartoService.CriarReserva"/>.
    /// 7. Retorna mensagem de sucesso ou falha.
    /// </remarks>
    public (string mensagem, bool sucesso) AtribuirQuarto(Hotel hotel, string clienteId, DateTime dataEntrada, DateTime dataSaida)
    {
        if (dataEntrada == default || dataSaida == default)
            return ("As datas nao podem ser vazias.", false);

        if (dataSaida <= dataEntrada)
            return ("A data de saida deve ser posterior à data de entrada.", false);

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

        Cliente? cliente = _clienteService.BuscarCliente(clienteId);
        if (cliente == null)
            return ("Cliente nao encontrado na sessao atual.", false);

        Quarto? quarto = _quartoService.BuscarQuarto(hotel.Nome, quartoDisponivel.Numero);
        if (quarto == null)
            return ("Quarto nao encontrado na sessao atual.", false);

        Reserva reserva = new Reserva(dataEntrada, dataSaida, hotel.Nome, clienteId, quartoDisponivel.Numero);

        // Adiciona reserva ao cliente e ao quarto, garantindo consistência de dados
        _clienteService.AdicionarReserva(reserva, cliente);
        _quartoService.CriarReserva(reserva, quarto);

        return ($"O quarto {quartoDisponivel.Numero} foi reservado com sucesso para {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}.", true);
    }
}
