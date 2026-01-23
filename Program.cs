HotelRepositorio hotelRepositorio = new();
HotelService hotelService = new HotelService(hotelRepositorio);
Menu menu = new(hotelService);

hotelService.CriarHotel("Parada Legal", "20");
hotelService.CriarHotel("Canto Divertido", "15");

menu.CriarUmHotel();
menu.ExibirHoteis();

Console.ReadKey();