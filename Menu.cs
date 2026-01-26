using System;

internal class Menu
{
	public Menu(HotelService _hotelService)
	{
		hotelService = _hotelService;
	}

	private readonly HotelService hotelService;

	public void CriarUmHotel()
	{
		Console.Write("Nome do Hotel: ");
		string nome = Console.ReadLine()!;
		Console.Write("QTD de Quartos: ");
		string qtdQuartos = Console.ReadLine()!;

		var resultado = hotelService.CriarHotel(nome, qtdQuartos);

		Console.WriteLine(resultado.mensagem);
	}

	public void ExibirHoteis()
	{
		List<Hotel> Hoteis = hotelService.ObterHoteis();

		foreach (Hotel hotel in Hoteis)
		{
			int qtdQuartos = hotel.Quartos.Count();

			Console.WriteLine($"Hotel: {hotel.Nome} | Quartos: {qtdQuartos}");
		}
	}

}
