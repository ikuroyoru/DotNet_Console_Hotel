using System.Reflection.Metadata.Ecma335;

namespace DotNet_Console_Hotel.Menus;

internal class MenuPrincipal : Menu
{
    public MenuPrincipal(HotelService hotelService)
        :base(hotelService) 
    {
        opcoes.Add(1, new MenuCriarReserva(hotelService));
        opcoes.Add(2, new MenuExibirHoteis(hotelService));
        opcoes.Add(3, new MenuAdicionarHotel(hotelService));
    }

    private Dictionary<int, Menu> opcoes = new();


    public override void Executar()
    {
        while (true)
        {
            Console.Clear();
            MostrarMenu();

            int escolha = ParseEscolha();

            if (opcoes.ContainsKey(escolha))
            {
                opcoes[escolha].Executar();
            }
            else
            {
                Console.WriteLine("Opcao invalida.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }


    int ParseEscolha()
    {
        int.TryParse(Console.ReadLine()!, out int escolha);
        return escolha;
    }

    void MostrarMenu()
    {
        Console.WriteLine("OPCOES INICIAIS");
        Console.WriteLine("1 - Fazer uma reserva");
        Console.WriteLine("2 - Exibir Hoteis");
        Console.WriteLine("3 - Inserir um Hotel");

    }
}
