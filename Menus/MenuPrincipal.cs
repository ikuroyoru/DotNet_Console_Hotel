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
/// 
/// Fluxo:
/// - Limpa o console a cada iteração.
/// - Exibe as opções disponíveis.
/// - Lê a escolha do usuário.
/// - Executa o menu correspondente caso exista.
/// - Exibe mensagem de erro caso a opção seja inválida.
/// - Aguarda uma tecla antes de reiniciar o loop.
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
    /// Executa o loop principal do menu.
    /// </summary>
    /// <remarks>
    /// Fluxo atual:
    /// - Limpa o console.
    /// - Exibe as opções do menu chamando <see cref="MostrarMenu"/>.
    /// - Lê a escolha do usuário via <see cref="ParseEscolha"/>.
    /// - Se a opção existir no dicionário <see cref="Opcoes"/>, executa o menu correspondente.
    /// - Caso contrário, exibe "Opcao invalida".
    /// - Aguarda uma tecla antes de reiniciar o loop.
    /// - O loop é contínuo e não possui condição de saída.
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
    /// Número digitado pelo usuário.
    /// Retorna 0 caso a conversão falhe.
    /// </returns>
    private int ParseEscolha()
    {
        int.TryParse(Console.ReadLine()!, out int escolha);
        return escolha;
    }

    /// <summary>
    /// Exibe as opções iniciais disponíveis no menu principal.
    /// </summary>
    /// <remarks>
    /// Opções atuais exibidas:
    /// 1 - Fazer uma reserva
    /// 2 - Exibir Hoteis
    /// 3 - Inserir um Hotel
    /// </remarks>
    private void MostrarMenu()
    {
        Console.WriteLine("OPCOES INICIAIS");
        Console.WriteLine("1 - Fazer uma reserva");
        Console.WriteLine("2 - Exibir Hoteis");
        Console.WriteLine("3 - Inserir um Hotel");
    }
}
