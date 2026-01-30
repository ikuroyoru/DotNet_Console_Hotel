namespace DotNet_Console_Hotel.Menus;

/// <summary>
/// Classe base para todos os menus da aplicação.
/// </summary>
/// <remarks>
/// Define comportamento comum que pode ser reutilizado
/// pelas classes derivadas.
/// Atualmente, o comportamento padrão consiste apenas
/// em limpar o console antes da execução do menu.
/// </remarks>
internal class Menu
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Menu"/>.
    /// </summary>
    public Menu() { }

    /// <summary>
    /// Executa o comportamento padrão do menu.
    /// </summary>
    /// <remarks>
    /// Limpa o console.
    /// 
    /// Pode ser sobrescrito por classes derivadas
    /// para implementar comportamentos específicos.
    /// </remarks>
    public virtual void Executar()
    {
        // Console.Clear();
    }
}
