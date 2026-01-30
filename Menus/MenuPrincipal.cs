using DotNet_Console_Hotel.Services;
using System.Reflection.Metadata.Ecma335;

namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Representa o menu principal da aplicação.
/// Responsável por exibir as opções iniciais e delegar a execução
/// para os menus correspondentes.
/// </summary>
/// <remarks>
/// O menu executa em loop infinito, aguardando interação do usuário.
/// A navegação ocorre com base em um dicionário que associa
/// números inteiros a instâncias de <see cref="Menu"/>.
/// </remarks>
internal class MenuPrincipal : Menu
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuPrincipal"/>.
    /// </summary>
    /// <param name="opcoes">
    /// Dicionário contendo as opções disponíveis no menu,
    /// onde a chave representa o número digitado pelo usuário
    /// e o valor representa o menu correspondente.
    /// </param>
    public MenuPrincipal(Dictionary<int, Menu> opcoes)
    {
        Opcoes = opcoes;
    }

    /// <summary>
    /// Dicionário que armazena as opções disponíveis no menu.
    /// </summary>
    private readonly Dictionary<int, Menu> Opcoes = new();

    /// <summary>
    /// Executa o menu principal.
    /// </summary>
    /// <remarks>
    /// Comportamento:
    /// - Limpa o console a cada iteração.
    /// - Exibe as opções disponíveis.
    /// - Lê a escolha do usuário.
    /// - Se a opção existir no dicionário, executa o menu correspondente.
    /// - Caso contrário, informa que a opção é inválida.
    /// - Aguarda uma tecla antes de reiniciar o loop.
    /// 
    /// O método executa indefinidamente (while true).
    /// </remarks>
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

    /// <summary>
    /// Lê a entrada do usuário e tenta convertê-la para inteiro.
    /// </summary>
    /// <returns>
    /// O número digitado pelo usuário.
    /// Caso a conversão falhe, retorna 0.
    /// </returns>
    int ParseEscolha()
    {
        int.TryParse(Console.ReadLine()!, out int escolha);
        return escolha;
    }

    /// <summary>
    /// Exibe as opções disponíveis no menu principal.
    /// </summary>
    /// <remarks>
    /// As opções exibidas são:
    /// 1 - Fazer uma reserva
    /// 2 - Exibir Hoteis
    /// 3 - Inserir um Hotel
    /// </remarks>
    void MostrarMenu()
    {
        Console.WriteLine("OPCOES INICIAIS");
        Console.WriteLine("1 - Fazer uma reserva");
        Console.WriteLine("2 - Exibir Hoteis");
        Console.WriteLine("3 - Inserir um Hotel");
    }
}
