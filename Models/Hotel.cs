using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DotNet_Console_Hotel.Models;

internal class Hotel
{
    public Hotel(string nome, string rua, int numero, string cidade, string estado, string pais, string cep, string telefone, int quantidadeQuartos)
    {
        this.Nome = nome;
        this.Rua = rua;
        this.Numero = numero;
        this.Cidade = cidade;
        this.Estado = estado;
        this.Cep = cep;
        this.Pais = pais;
        this.Telefone = telefone;
        this.QuantidadeQuartos = quantidadeQuartos;
    }

    public Guid Id { get; set; }
    public string Nome { get; private set; }
    public string Rua { get; private set; }
    public int Numero { get; private set; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Cep { get; private set; }
    public string Pais { get; }
    public string Telefone { get; private set; }
    public int QuantidadeQuartos { get; private set; }
    public bool Ativo { get; private set; }

    public List<Quarto> GerarQuartos(int qtdSingle, int qtdDouble, int qtdTriple, int qtdSuite, decimal precoSingle, decimal precoDouble, decimal precoTriple, decimal precoSuite)
    {
        string[] categorias = { "Single", "Double", "Triple", "Suite" };

        List<Quarto> quartos = new List<Quarto>();
        int numero = 1;

        for (int i = 0; i < qtdSingle; i++)
            quartos.Add(new Quarto(numero++, precoSingle, "Single", Id));

        for (int i = 0; i < qtdDouble; i++)
            quartos.Add(new Quarto(numero++, precoDouble, "Double", Id));

        for (int i = 0; i < qtdTriple; i++)
            quartos.Add(new Quarto(numero++, precoTriple, "Triple", Id));

        for (int i = 0; i < qtdSuite; i++)
            quartos.Add(new Quarto(numero++, precoSuite, "Suite", Id));

        foreach (var quarto in quartos)
            Console.WriteLine($"Quarto {quarto.Numero} - Hotel: {Id}");

        return quartos;
    }
    public void DesativarHotel()
    {
        Ativo = false;
    }
}
