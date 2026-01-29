internal class Hotel
{
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
    
    public string Nome { get; private set; }

    private readonly List<Quarto> _quartos = new();
    public IReadOnlyList<Quarto> Quartos => _quartos;

    // List<Avaliacao> Avaliacoes { get; set; } AVALIACOES PARA HOTEIS
}
