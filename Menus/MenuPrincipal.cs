using DotNet_Console_Hotel.Services;
using System.Reflection.Metadata.Ecma335;

namespace DotNet_Console_Hotel.Menus;

internal class MenuPrincipal : Menu
{
    public MenuPrincipal(Dictionary<int, Menu> opcoes)
    {
        Opcoes = opcoes;
    }
    private readonly Dictionary<int, Menu> Opcoes = new();

    public override void Executar()
    {
        while (true)
        {
            Console.Clear();
            MostrarMenu();

            int escolha = ParseEscolha();

            if (Opcoes.ContainsKey(escolha))
            {
                Opcoes[escolha].Executar();
            }
            else
            {
                Console.WriteLine("Opcao invalida.");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    private int ParseEscolha()
    {
        int.TryParse(Console.ReadLine()!, out int escolha);
        return escolha;
    }

    private void MostrarMenu()
    {
        Console.WriteLine("OPCOES INICIAIS");
        Console.WriteLine("1 - Fazer uma reserva");
        Console.WriteLine("2 - Exibir Hoteis");
        Console.WriteLine("3 - Exibir Minhas Informacoes");
    }
}
