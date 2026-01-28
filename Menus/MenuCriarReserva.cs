namespace DotNet_Console_Hotel.Menus;

internal class MenuCriarReserva : Menu
{
    public MenuCriarReserva(HotelService hotelService)
        :base(hotelService) 
    { }

    public override void Executar()
    {
        base.Executar();
        Console.WriteLine("FAZER UMA RESERVA");
    }
}
