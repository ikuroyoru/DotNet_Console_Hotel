using DotNet_Console_Hotel.Menus;

HotelRepositorio hotelRepositorio = new();
HotelService hotelService = new HotelService(hotelRepositorio);
Menu menu = new(hotelService);

MenuPrincipal menuPrincipal = new(hotelService);

hotelService.CriarHotel("Parada Legal", "20");
hotelService.CriarHotel("Canto Divertido", "15");

menuPrincipal.Executar();

Console.ReadKey();