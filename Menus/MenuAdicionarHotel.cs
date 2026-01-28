namespace DotNet_Console_Hotel.Menus;

internal class MenuAdicionarHotel : Menu
{
    public MenuAdicionarHotel(HotelService hotelService)
        :base(hotelService)
	{
	}

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
