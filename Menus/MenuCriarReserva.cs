using DotNet_Console_Hotel.Services;
using System.Runtime.ExceptionServices;

namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Menu responsável pelo fluxo de criação de uma reserva.
/// </summary>
/// <remarks>
/// Fluxo executado:
/// - Permite ao usuário selecionar um hotel.
/// - Define datas de entrada e saída.
/// - Solicita ao <see cref="ReservaService"/> a atribuição de um quarto.
/// - Exibe o resultado da operação no console.
/// 
/// A seleção de datas atualmente não é interativa.
/// Datas são definidas automaticamente pelos métodos internos.
/// </remarks>
internal class MenuCriarReserva : Menu
{
    private readonly HotelService _hotelService;

    /// <summary>
    /// Serviço responsável por atribuir quartos disponíveis.
    /// </summary>
    /// <remarks>
    /// Instância criada internamente na classe.
    /// </remarks>
    ReservaService reservaService = new();

    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuCriarReserva"/>.
    /// </summary>
    /// <param name="hotelService">
    /// Serviço responsável por fornecer os hotéis cadastrados.
    /// </param>
    /// <param name="reservaService">
    /// Parâmetro recebido no construtor, mas não utilizado internamente.
    /// </param>
    public MenuCriarReserva(HotelService hotelService, ReservaService reservaService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Executa o fluxo de criação de reserva.
    /// </summary>
    /// <remarks>
    /// Comportamento:
    /// - Executa comportamento base da classe Menu.
    /// - Solicita seleção de hotel.
    /// - Caso o usuário cancele, retorna ao menu anterior.
    /// - Define datas de entrada e saída.
    /// - Solicita ao serviço a atribuição de um quarto.
    /// - Exibe mensagem de sucesso ou erro.
    /// </remarks>
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

        var reserva = reservaService.AtribuirQuarto(hotelEscolhido, dataEntrada, dataSaida);

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
    /// O hotel selecionado ou null caso o usuário pressione ESC.
    /// </returns>
    /// <remarks>
    /// O método:
    /// - Exibe a lista de hotéis disponíveis.
    /// - Solicita o nome do hotel.
    /// - Realiza busca exata pelo nome.
    /// - Permite cancelar pressionando a tecla ESC.
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
    /// Define a data de entrada da reserva.
    /// </summary>
    /// <returns>
    /// Data atual do sistema.
    /// </returns>
    /// <remarks>
    /// Atualmente não há interação com o usuário.
    /// </remarks>
    DateTime SelecionarDataInicio()
    {
        return DateTime.Now;
    }

    /// <summary>
    /// Define a data de saída da reserva.
    /// </summary>
    /// <returns>
    /// Data atual acrescida de 7 dias.
    /// </returns>
    /// <remarks>
    /// Atualmente não há interação com o usuário.
    /// </remarks>
    DateTime SelecionarDataFim()
    {
        return DateTime.Now.AddDays(7);
    }
}
