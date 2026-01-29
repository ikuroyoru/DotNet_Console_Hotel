using DotNet_Console_Hotel.Services;
using System.Runtime.ExceptionServices;

namespace DotNet_Console_Hotel.Menus;

internal class MenuCriarReserva : Menu
{
    public MenuCriarReserva(HotelService hotelService, ReservaService reservaService)
    { 
     _hotelService = hotelService;
    }

    ReservaService reservaService = new();
    HotelService _hotelService;
    // Menu - Hotel - Quarto - Reserva
    // Pega o hotel. Dentro do hotel, pega os quartos disponiveis. Dentro de quarto, cria a reserva.


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
        else Console.WriteLine($"\n{reserva.mensagem}");
    }

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

    DateTime SelecionarDataInicio()
    {
        // DESENVOLVER FUNCAO PARA O USUARIO DECIDIR A DATA
        return DateTime.Now;
    }

    DateTime SelecionarDataFim()
    {
        // DESENVOLVER FUNCAO PARA O USUARIO DECIDIR A DATA
        return DateTime.Now.AddDays(7);
    }

    

}
