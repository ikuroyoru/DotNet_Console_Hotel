using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Menu responsável pelo fluxo de criação de uma reserva para um usuário logado.
/// </summary>
/// <remarks>
/// Este menu coordena a criação de reservas, delegando a lógica de atribuição de quartos
/// ao <see cref="ReservaService"/> e utilizando o <see cref="SessaoService"/>
/// para obter o usuário atualmente autenticado.
/// 
/// Fluxo:
/// - Permite ao usuário selecionar um hotel disponível.
/// - Define datas de entrada e saída (automaticamente no momento).
/// - Solicita ao <see cref="ReservaService"/> a criação da reserva.
/// - Exibe o resultado da operação no console.
/// 
/// Observações:
/// - A seleção de datas não é interativa; datas são definidas pelos métodos internos.
/// - Caso o usuário cancele a seleção de hotel, retorna ao menu anterior.
/// - O menu não valida login; assume que o <see cref="SessaoService"/> já gerencia o estado de autenticação.
/// </remarks>
internal class MenuCriarReserva : Menu
{
    private readonly HotelService _hotelService;

    ReservaService _reservaService;

    public MenuCriarReserva(HotelService hotelService, ReservaService reservaService)
    {
        _hotelService = hotelService;
        _reservaService = reservaService;
    }

    public override void Executar()
    {
        base.Executar();

        var hotelEscolhido = SelecionarHotel();

        if (hotelEscolhido == null)
        {
            Console.WriteLine("Retornando ao Menu Inicial...");
            return;
        }

        Console.WriteLine($"SELECIONANDO DATA PARA RESERVA NO HOTEL : {hotelEscolhido.Nome}");

        DateTime dataEntrada = SelecionarDataInicio();
        Console.WriteLine($"Data entrada: {dataEntrada} ");

        DateTime dataSaida = SelecionarDataFim();
        Console.WriteLine($"Data saida: {dataSaida} ");

        var reserva = _reservaService.AtribuirQuarto(hotelEscolhido, dataEntrada, dataSaida);

        if (!reserva.sucesso)
        {
            Console.WriteLine("\nHouve um erro inesperado: " + reserva.mensagem);
        }
        else
        {
            Console.WriteLine($"\n{reserva.mensagem}");
        }
    }

    /// <summary>
    /// Permite ao usuário selecionar um hotel pelo nome.
    /// </summary>
    /// <returns>
    /// O hotel selecionado ou <c>null</c> caso o usuário cancele pressionando ESC.
    /// </returns>
    /// <remarks>
    /// - Exibe a lista de hotéis disponíveis.
    /// - Solicita o nome do hotel para seleção.
    /// - Busca o hotel pelo nome exato.
    /// - Permite cancelar a operação pressionando ESC.
    /// </remarks>
    Hotel? SelecionarHotel()
    {
        IReadOnlyList<Hotel> hoteis = _hotelService.ObterHoteis();

        while (true)
        {
            Console.Clear();

            Console.WriteLine("FAZER UMA RESERVA\n\nHOTEIS DISPONIVEIS\n");

            foreach (Hotel hotel in hoteis)
            {
                Console.WriteLine(hotel.Nome);
            }

            Console.Write("\nDigite o Hotel que quer fazer a reserva: ");
            string escolhaHotel = Console.ReadLine()!;

            var hotelEscolhido = hoteis.FirstOrDefault(hotel => hotel.Nome == escolhaHotel);

            if (hotelEscolhido != null)
            {
                Console.WriteLine($"Hotel {hotelEscolhido.Nome} selecionado");
                return hotelEscolhido;
            }

            Console.Clear();
            Console.WriteLine($"Hotel '{escolhaHotel}' nao existe.");
            Console.WriteLine("\nPressione ESC para retornar ao Menu \nPressione qualquer tecla para tentar novamente");
            var tecla = Console.ReadKey();

            if (tecla.Key == ConsoleKey.Escape)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Retorna a data de início da reserva.
    /// </summary>
    /// <returns>Data atual do sistema.</returns>
    /// <remarks>
    /// Atualmente o método não permite interação do usuário e retorna sempre DateTime.Now.
    /// </remarks>
    DateTime SelecionarDataInicio()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// Retorna a data de término da reserva.
    /// </summary>
    /// <returns>Data atual acrescida de 7 dias.</returns>
    /// <remarks>
    /// Atualmente o método não permite interação do usuário.
    /// </remarks>
    DateTime SelecionarDataFim()
    {
        return DateTime.Now.AddDays(7);
    }
}
