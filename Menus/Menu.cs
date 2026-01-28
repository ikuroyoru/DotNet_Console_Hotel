namespace DotNet_Console_Hotel.Menus;

internal class Menu
{
	public Menu(HotelService hotelService)
	{
		_hotelService = hotelService;
	}

	protected readonly HotelService _hotelService;

	public virtual void Executar()
	{
		Console.Clear();
	}
}
