using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Models;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

internal class ServiceReservation
{
    private readonly ServiceRoom _quartoService; // Change by RepositoryRoom
    private readonly ServiceSession _serviceSession;
    private readonly RepositoryReservation _repositoryReservation;

    public ServiceReservation(ServiceSession serviceSession, ServiceRoom serviceRoom, RepositoryReservation repositoryReservation)
    {
        _quartoService = serviceRoom;
        _serviceSession = serviceSession;
        _repositoryReservation = repositoryReservation;
    }

    /*
    public (string mensagem, bool sucesso) CriarReserva(Hotel hotel, DateTime dataEntrada, DateTime dataSaida)
    {
        var usuarioLogado = _sessaoService.ObterUsuarioLogado();
        var userId = usuarioLogado.userId;

        if (!usuarioLogado.status)
            return ("Usuario nao autenticado. Por favor, faça login para reservar um quarto.", false);

        if (dataEntrada == default || dataSaida == default)
            return ("As datas nao podem ser vazias.", false);

        if (dataSaida <= dataEntrada)
            return ("A data de saida deve ser posterior à data de entrada.", false);


        // DEVE OBTER OS QUARTOS DE UM HOTEL COM QuartoService
        // FILTRA OS QUARTOS DO HOTEL PARA ENCONTRAR UM QUE NAO TENHA RESERVAS CONFLITANTES COM AS DATAS DE ENTRADA E SAIDA SELECIONADAS PELO USUÁRIO
        // FILTRO DEVE OCORRER EM RESERVAREPOSITORIO
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
        Reserva reserva = new Reserva(dataEntrada, dataSaida, userId, quartoDisponivel.id); // NECESSARIO ASSOCIAR O HOTEL.ID E NAO HOTEL.NOME

        _reservaRepositorio.AdicionarReserva(reserva);

        return ($"O quarto {quartoDisponivel.id} foi reservado com sucesso para {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}.", true);
    }
    */
}
