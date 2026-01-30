namespace DotNet_Console_Hotel.Models;

/// <summary>
/// Representa um hotel contendo um conjunto de quartos.
/// </summary>
/// <remarks>
/// Ao ser criado, o hotel armazena informações básicas, como nome,
/// e mantém uma lista de quartos associados.
///
/// Regras de negócio aplicadas:
/// - O hotel deve possuir pelo menos um quarto.
/// - A lista de quartos é gerenciada internamente.
/// - A numeração e categorias dos quartos (Standard/Premium) podem ser definidas
///   externamente ou via serviço responsável por criar quartos.
/// </remarks>
internal class Hotel
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Hotel"/>.
    /// </summary>
    /// <param name="nome">Nome do hotel.</param>
    /// <param name="quartos">Lista de quartos a ser associada ao hotel.</param>
    /// <exception cref="ArgumentException">
    /// Lançada quando a lista de quartos é nula ou vazia.
    /// </exception>
    public Hotel(string nome, List<Quarto> quartos)
    {
        Nome = nome;
        foreach (var quarto in quartos)
        {
            _quartos.Add(quarto);
        }
    }

    /// <summary>
    /// Nome do hotel.
    /// </summary>
    public string Nome { get; private set; }

    private readonly List<Quarto> _quartos = new();

    /// <summary>
    /// Lista somente leitura contendo todos os quartos do hotel.
    /// </summary>
    /// <remarks>
    /// A lista é protegida contra modificações externas.
    /// Para adicionar ou alterar quartos, utilize os serviços correspondentes.
    /// </remarks>
    public IReadOnlyList<Quarto> Quartos => _quartos;

}
