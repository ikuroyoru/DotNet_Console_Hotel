namespace DotNet_Console_Hotel.Menus;

using DotNet_Console_Hotel.Models;

/// <summary>
/// Menu responsável por exibir a lista de hotéis cadastrados no sistema.
/// </summary>
/// <remarks>
/// Este menu consulta o <see cref="HotelService"/> para obter
/// os hotéis armazenados e apresenta suas informações no console.
///
/// Comportamento atual:
/// - Exibe um título de seção "LISTA DE HOTEIS".
/// - Obtém todos os hotéis por meio do serviço.
/// - Para cada hotel, exibe o nome e a quantidade total de quartos cadastrados.
/// - Caso não existam hotéis cadastrados, apenas o título será exibido.
/// </remarks>
internal class MenuExibirHoteis : Menu
{
    private readonly HotelService _hotelService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuExibirHoteis"/>.
    /// </summary>
    /// <param name="hotelService">
    /// Serviço responsável por fornecer os hotéis cadastrados.
    /// </param>
    public MenuExibirHoteis(HotelService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Executa a exibição dos hotéis no console.
    /// </summary>
    /// <remarks>
    /// Fluxo:
    /// - Exibe o título do menu.
    /// - Recupera a lista de hotéis cadastrados do <see cref="HotelService"/>.
    /// - Itera sobre cada hotel e exibe seu nome e quantidade de quartos.
    /// </remarks>
    public override void Executar()
    {
        Console.WriteLine("LISTA DE HOTEIS");

        IReadOnlyList<Hotel> Hoteis = _hotelService.ObterHoteis();

        

        foreach (Hotel hotel in Hoteis)
        {
            Console.WriteLine($"Hotel: {hotel.Nome} | Quartos: {hotel.Quartos.Count()}");
        }
    }
}
