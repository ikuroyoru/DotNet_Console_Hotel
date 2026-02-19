using DotNet_Console_Hotel.Models;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

internal class ReservaService
{
    private readonly QuartoService _quartoService;
    private readonly ClienteService _clienteService;
    private readonly SessaoService _sessaoService;

    public ReservaService(SessaoService sessaoService, QuartoService quartoService, ClienteService clienteService)
    {
        _quartoService = quartoService;
        _clienteService = clienteService;
        _sessaoService = sessaoService;
    }

    public (string mensagem, bool sucesso) AtribuirQuarto(Hotel hotel, DateTime dataEntrada, DateTime dataSaida)
    {
        var usuarioLogado = _sessaoService.ObterUsuarioLogado();
        var userId = usuarioLogado.userId;

        if (!usuarioLogado.status)
            return ("Usuario nao autenticado. Por favor, faça login para reservar um quarto.", false);

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
            return ($"Nenhum quarto disponivel para o periodo {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}. Por favor, selecione outra data.", false);


        // Cria a reserva com dados verificados e consistentes. Reserva nao e criada se o usuario nao estiver autenticado ou se o cliente nao for encontrado
        Reserva reserva = new Reserva(dataEntrada, dataSaida, hotel.Nome, userId, quartoDisponivel.Numero); // NECESSARIO ASSOCIAR O HOTEL.ID E NAO HOTEL.NOME

        // UMA RESERVA DEVE SER CRIADA COM IDS E NAO COM OBJETOS DE CLIENTE OU QUARTO. - DEVE TER QUARTO.ID / USER.ID
        var reservaCliente = _clienteService.AtribuirReserva(reserva, userId);

        if (reservaCliente)
        {
            _quartoService.AtribuirReserva(reserva, quartoDisponivel);
            return ($"O quarto {quartoDisponivel.Numero} foi reservado com sucesso para {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}.", true);
        }
        else return ("Ocorreu um erro ao realizar a reserva. Por favor, tente novamente.", false);
    }
}
