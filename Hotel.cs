internal class Hotel
{
    public Hotel(string nome, int qtdQuartos)
    {
        if (qtdQuartos <= 0)
            throw new ArgumentException("Hotel deve possuir ao menos um quarto");

        Nome = nome;
        Quartos = new List<Quarto>();

        double premium = 0.2; // 20% de quartos premium

        int qtdPremium = (int)Math.Round(qtdQuartos * premium);
        int qtdStandard = qtdQuartos - qtdPremium;

        for (int i = 1; i <= qtdStandard; i++)
            Quartos.Add(new Quarto(i, "Standard"));

        for (int i = qtdStandard + 1; i <= qtdQuartos; i++)
            Quartos.Add(new Quarto(i, "Premium"));

        ExibirQuartos();
    }

    public string Nome { get; private set; }
    public List<Quarto> Quartos { get; private set; }
    // List<Avaliacao> Avaliacoes { get; set; } AVALIACOES PARA HOTEIS

    void ExibirQuartos()
    {
        foreach (var quarto in Quartos)
        {
            Console.WriteLine($"Quarto {quarto.Numero} | Categoria: {quarto.Categoria}");
        }
    }
}
