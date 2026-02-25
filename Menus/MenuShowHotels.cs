namespace DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Models;

internal class MenuShowHotels : Menu
{
    private readonly ServiceHotel _serviceHotel;

    public MenuShowHotels(ServiceHotel serviceHotel)
    {
        _serviceHotel = serviceHotel;
    }

    public override void Execute()
    {
        int hotelLoadCount = 10;

        Console.WriteLine("**** Hotel List ****");
        IReadOnlyList<Hotel> hotels = _serviceHotel.GetHotels(hotelLoadCount);

        foreach (Hotel hotel in hotels)
        {
            Console.WriteLine($"Hotel: {hotel.Name}");
        }
    }
}
