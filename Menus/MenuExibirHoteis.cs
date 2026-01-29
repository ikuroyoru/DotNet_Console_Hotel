namespace DotNet_Console_Hotel.Menus;

internal class MenuExibirHoteis : Menu
{
	public MenuExibirHoteis(HotelService hotelService) 
    { 
      _hotelService = hotelService;
    }

    private readonly HotelService _hotelService;
    public override void Executar()
	{
        Console.WriteLine("LISTA DE HOTEIS");
        IReadOnlyList<Hotel> Hoteis = _hotelService.ObterHoteis();

        foreach (Hotel hotel in Hoteis)
        {
            int qtdQuartos = hotel.Quartos.Count();

            Console.WriteLine($"Hotel: {hotel.Nome} | Quartos: {qtdQuartos}");
        }
    }
}
