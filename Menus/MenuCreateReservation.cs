using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Menus;
internal class MenuCreateReservation : Menu
{
    private readonly ServiceHotel _serviceHotel;

    ServiceReservation _serviceReservation;

    public MenuCreateReservation(ServiceHotel serviceHotel, ServiceReservation serviceReservation)
    {
        _serviceHotel = serviceHotel;
        _serviceReservation = serviceReservation;
    }

    public override void Execute()
    {
        base.Execute();

        var selectedHotel = SelectHotel();

        if (selectedHotel == null)
        {
            Console.WriteLine("Returning to Main Menu...");
            return;
        }

        Console.WriteLine($"Selecting CheckIn and CheckOut date : {selectedHotel.Name}");

        DateTime checkIn = SelectCheckInDate();
        Console.WriteLine($"CheckIn Date: {checkIn} ");

        DateTime checkOut = SelectCheckOutDate();
        Console.WriteLine($"CheckOut Date: {checkOut} ");

        var createReservation = _serviceReservation.CreateReservation(selectedHotel, checkIn, checkOut);

        if (!createReservation.Success) Console.WriteLine(createReservation.Error);

    }
    Hotel? SelectHotel()
    {
        int hotelLoadCount = 10;

        IReadOnlyList<Hotel> hoteis = _serviceHotel.GetHotels(hotelLoadCount);

        while (true)
        {
            Console.Clear();

            Console.WriteLine("Make a reservation\n\nAvaliable Hotels\n");

            foreach (Hotel hotel in hoteis)
            {
                Console.WriteLine(hotel.Name);
            }

            Console.Write("\nName of the Hotel you want to CheckIn: ");
            string selectedHotelInString = Console.ReadLine()!;

            var selectedHotel = hoteis.FirstOrDefault(hotel => hotel.Name == selectedHotelInString);

            if (selectedHotel != null)
            {
                Console.WriteLine($"Hotel {selectedHotel.Name} selected");
                return selectedHotel;
            }

            Console.Clear();
            Console.WriteLine($"Hotel '{selectedHotelInString}' doesnt exists");
            Console.WriteLine("\nPress ESC to get back into the main menu OR Press ANY KEY to try again");
            var key = Console.ReadKey();

            if (key.Key == ConsoleKey.Escape)
            {
                return null;
            }
        }
    }
    DateTime SelectCheckInDate()
    {
        return DateTime.UtcNow;
    }
    DateTime SelectCheckOutDate()
    {
        return DateTime.UtcNow.AddDays(7);
    }
}
