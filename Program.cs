using DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Services;

var hotelRepositorio = new HotelRepositorio();
var hotelService = new HotelService(hotelRepositorio);
var reservaService = new ReservaService();


var opcoes = new Dictionary<int, Menu>
{
    { 1, new MenuCriarReserva(hotelService, reservaService) },
    { 2, new MenuExibirHoteis(hotelService) },
    { 3, new MenuAdicionarHotel(hotelService) }
};


var menu = new Menu();
var menuPrincipal = new MenuPrincipal(opcoes);


hotelService.CriarHotel("Parada Legal", "20");
hotelService.CriarHotel("Canto Divertido", "15");

menuPrincipal.Executar();

Console.ReadKey();