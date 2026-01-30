/*
 Ponto de entrada da aplicação Console Hotel.

 Fluxo de inicialização:

 - Instancia o repositório em memória (HotelRepositorio).
 - Instancia os serviços (HotelService e ReservaService).
 - Configura o dicionário de menus disponíveis.
 - Cria o MenuPrincipal com as opções configuradas.
 - Adiciona dois hotéis iniciais para demonstração.
 - Inicia a execução do menu principal.

 Observações importantes:

 - O armazenamento é exclusivamente em memória.
 - Dois hotéis são criados automaticamente ao iniciar o sistema.
 - O MenuPrincipal executa em loop infinito.
 - O Console.ReadKey() ao final não será alcançado,
   pois o menu principal nunca encerra a execução.
*/


using DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Repositorios;
using DotNet_Console_Hotel.Models;

var sessaoService = new SessaoService();
var hotelRepositorio = new HotelRepositorio();
var quartoRepositorio = new QuartoRepositorio();
var quartoService = new QuartoService(quartoRepositorio);
var hotelService = new HotelService(hotelRepositorio, quartoService);
var clienteRepositorio = new ClienteRepositorio(); 
var clienteService = new ClienteService(clienteRepositorio);
var reservaService = new ReservaService(quartoService, clienteService);
var autenticacaoService = new AutenticacaoService(clienteService, sessaoService);
var menu = new Menu();


var opcoes = new Dictionary<int, Menu>
{
    { 1, new MenuCriarReserva(hotelService, reservaService, sessaoService) },
    { 2, new MenuExibirHoteis(hotelService) },
    { 3, new MenuAdicionarHotel(hotelService) }
    //{ 4, new ExibirReservar() } // Exibe as reservas do usuario atualmente logado
    //{ 5, new CancelarReservar() } // Cencela uma reserva selecionada pelo usuario logado, atualiza os status conforme o cancelamento
    //{ 6, new MenuSair() } // Opcional: Implementar um menu para sair da aplicação
};

var menuPrincipal = new MenuPrincipal(opcoes);
var menuAutenticacao = new MenuAutenticacao(autenticacaoService, menuPrincipal);

clienteService.AdicionarCliente("Admin", "000.000.000-00", "admin123");

hotelService.CriarHotel("Parada Legal", "20");
hotelService.CriarHotel("Canto Divertido", "15");

menuAutenticacao.Executar();

Console.ReadKey();