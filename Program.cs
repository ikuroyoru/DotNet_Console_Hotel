using Npgsql;
using DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Repositorios;

var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=nihon";
var connection = new NpgsqlConnection(connectionString);
connection.Open();

var sessaoService = new SessaoService();
var hotelRepositorio = new HotelRepositorio(connection);
var quartoRepositorio = new QuartoRepositorio(connection);
var clienteRepositorio = new ClienteRepositorio(connection);
var reservaRepositorio = new ReservaRepositorio(connection);

var quartoService = new QuartoService(quartoRepositorio);
var hotelService = new HotelService(hotelRepositorio, quartoService);
var clienteService = new ClienteService(clienteRepositorio);
var reservaService = new ReservaService(sessaoService, quartoService, reservaRepositorio);
var autenticacaoService = new AutenticacaoService(sessaoService, clienteService);

var menu = new Menu();


var opcoes = new Dictionary<int, Menu>
{
    { 1, new MenuCriarReserva(hotelService, reservaService) },
    { 2, new MenuExibirHoteis(hotelService) },
    { 3, new MenuAdicionarHotel(hotelService) }
    //{ 4, new ExibirReservar() } // Exibe as reservas do usuario atualmente logado
    //{ 5, new CancelarReservar() } // Cencela uma reserva selecionada pelo usuario logado, atualiza os status conforme o cancelamento
    //{ 6, new MenuSair() } // Opcional: Implementar um menu para sair da aplicação
};

var menuPrincipal = new MenuPrincipal(opcoes);
var menuAutenticacao = new MenuAutenticacao(autenticacaoService, menuPrincipal);

clienteService.NovoCliente("Admin", "000.000.000-00", "admin123");

hotelService.CriarHotel("Parada Legal", "20");
hotelService.CriarHotel("Canto Divertido", "15");

menuAutenticacao.Executar();

Console.ReadKey();


