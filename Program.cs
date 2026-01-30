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