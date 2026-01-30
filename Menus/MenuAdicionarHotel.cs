namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Menu responsável pela adição de novos hotéis ao sistema.
/// </summary>
/// <remarks>
/// Este menu coleta informações do usuário (nome do hotel e quantidade de quartos)
/// e delega a criação do hotel para o <see cref="HotelService"/>.
/// A validação dos dados e a criação efetiva do hotel são totalmente
/// responsabilidade do serviço.
/// </remarks>
internal class MenuAdicionarHotel : Menu
{
    private readonly HotelService _hotelService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuAdicionarHotel"/>.
    /// </summary>
    /// <param name="hotelService">
    /// Serviço utilizado para criar o hotel e validar os dados informados.
    /// </param>
    public MenuAdicionarHotel(HotelService hotelService)
    {
        _hotelService = hotelService;
    }

    /// <summary>
    /// Executa o fluxo de adição de um hotel.
    /// </summary>
    /// <remarks>
    /// Fluxo atual:
    /// - Exibe o título do menu.
    /// - Solicita ao usuário o nome do hotel e a quantidade de quartos.
    /// - Chama <see cref="HotelService.CriarHotel(string, string)"/> com os dados informados.
    /// - Exibe a mensagem retornada pelo serviço.
    /// 
    /// Observações:
    /// - Nenhuma validação de entrada é feita neste menu; tudo é delegado ao serviço.
    /// - Base da classe <see cref="Menu"/> é executado antes do fluxo específico.
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
