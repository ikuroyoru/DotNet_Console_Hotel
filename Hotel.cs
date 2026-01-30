/// <summary>
/// Representa um hotel contendo um conjunto fixo de quartos.
/// Ao ser criado, o hotel distribui automaticamente os quartos
/// em categorias Standard e Premium.
/// </summary>
/// <remarks>
/// Regra de negócio:
/// - O hotel deve possuir pelo menos um quarto.
/// - 20% dos quartos são classificados como Premium.
/// - O restante dos quartos são Standard.
/// - A numeração dos quartos é sequencial iniciando em 1.
/// </remarks>
internal class Hotel
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Hotel"/>.
    /// </summary>
    /// <param name="nome">Nome do hotel.</param>
    /// <param name="qtdQuartos">Quantidade total de quartos.</param>
    /// <exception cref="ArgumentException">
    /// Lançada quando a quantidade de quartos é menor ou igual a zero.
    /// </exception>
    public Hotel(string nome, int qtdQuartos)
    {
        if (qtdQuartos <= 0)
            throw new ArgumentException("Hotel deve possuir ao menos um quarto");

        Nome = nome;
        _quartos = new List<Quarto>();

        double premium = 0.2; // 20% de quartos premium

        int qtdPremium = (int)Math.Round(qtdQuartos * premium);
        int qtdStandard = qtdQuartos - qtdPremium;

        for (int i = 1; i <= qtdStandard; i++)
            _quartos.Add(new Quarto(i, "Standard"));

        for (int i = qtdStandard + 1; i <= qtdQuartos; i++)
            _quartos.Add(new Quarto(i, "Premium"));
    }

    /// <summary>
    /// Nome do hotel.
    /// </summary>
    public string Nome { get; private set; }

    private readonly List<Quarto> _quartos = new();

    /// <summary>
    /// Lista somente leitura contendo todos os quartos do hotel.
    /// </summary>
    public IReadOnlyList<Quarto> Quartos => _quartos;
}
