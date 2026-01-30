namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Menu responsável por exibir a lista de hotéis cadastrados.
/// </summary>
/// <remarks>
/// Este menu consulta o <see cref="HotelService"/> para obter
/// os hotéis armazenados e exibe suas informações no console.
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
    /// Executa a exibição da lista de hotéis.
    /// </summary>
    /// <remarks>
    /// Comportamento:
    /// - Exibe um título no console.
    /// - Obtém todos os hotéis por meio do serviço.
    /// - Para cada hotel, exibe:
    ///   - Nome
    ///   - Quantidade total de quartos
    /// 
    /// Caso não existam hotéis cadastrados,
    /// nada será exibido além do título.
    /// </remarks>
    public override void Executar()
    {
        Console.WriteLine("LISTA DE HOTEIS");

        IReadOnlyList<Hotel> Hoteis = _hotelService.ObterHoteis();

        foreach (Hotel hotel in Hoteis)
        {
            int qtdQuartos = hotel.Quartos.Count();

            Console.WriteLine($"Hotel: {hotel.Nome} | Quartos: {qtdQuartos}");
        }
    }
}
