namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Menu responsável pela criação de novos hotéis.
/// </summary>
/// <remarks>
/// Este menu coleta os dados informados pelo usuário
/// e delega a criação do hotel ao <see cref="HotelService"/>.
/// Não realiza validações diretamente.
/// </remarks>
internal class MenuAdicionarHotel : Menu
{
    private readonly HotelService _hotelService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuAdicionarHotel"/>.
    /// </summary>
    /// <param name="hotelService">
    /// Serviço responsável por validar e criar o hotel.
    /// </param>
    public MenuAdicionarHotel(HotelService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Executa o fluxo de criação de hotel.
    /// </summary>
    /// <remarks>
    /// Comportamento:
    /// - Executa o comportamento base da classe Menu.
    /// - Solicita ao usuário o nome do hotel.
    /// - Solicita a quantidade de quartos.
    /// - Envia os dados ao <see cref="HotelService"/>.
    /// - Exibe a mensagem retornada pelo serviço.
    /// 
    /// A validação dos dados (nome vazio, quantidade inválida, etc.)
    /// é realizada exclusivamente pelo serviço.
    /// </remarks>
    public override void Executar()
    {
        base.Executar();

        Console.WriteLine("ADICIONAR UM HOTEL");

        Console.Write("Nome do Hotel: ");
        string nome = Console.ReadLine()!;

        Console.Write("QTD de Quartos: ");
        string qtdQuartos = Console.ReadLine()!;

        var resultado = _hotelService.CriarHotel(nome, qtdQuartos);

        Console.WriteLine(resultado.mensagem);
    }
}
