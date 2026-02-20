using DotNet_Console_Hotel;
using DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;
using DotNet_Console_Hotel.Services;

var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=nihon";

var hotelRepositorio = new HotelRepositorio(connectionString);
var quartoRepositorio = new QuartoRepositorio(connectionString);
var clienteRepositorio = new ClienteRepositorio(connectionString);
var reservaRepositorio = new ReservaRepositorio(connectionString);

csvReader reader = new(hotelRepositorio, quartoRepositorio);

var sessaoService = new SessaoService();
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
    //{ 4, new ExibirReservar() } // Exibe as reservas do usuario atualmente logado
    //{ 5, new CancelarReservar() } // Cencela uma reserva selecionada pelo usuario logado, atualiza os status conforme o cancelamento
    //{ 6, new MenuSair() } // Opcional: Implementar um menu para sair da aplicação
};

var menuPrincipal = new MenuPrincipal(opcoes);
var menuAutenticacao = new MenuAutenticacao(autenticacaoService, menuPrincipal);

clienteService.NovoCliente("Admin", "000.000.000-00", "admin123");

reader.HotelReader();

// menuAutenticacao.Executar();

Console.ReadKey();


