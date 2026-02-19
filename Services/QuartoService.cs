using System;
using System.Collections.Generic;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;

namespace DotNet_Console_Hotel.Services;

internal class QuartoService
{
    private readonly QuartoRepositorio _quartoRepositorio;

    public QuartoService(QuartoRepositorio quartoRepositorio)
    {
        _quartoRepositorio = quartoRepositorio;
    }

    public Quarto? BuscarQuarto(string hotelId, int numero)
    {
        return _quartoRepositorio.ObterPorNumero(hotelId, numero);
    }

    public List<Quarto> GerarQuartos(int quantidadeDeQuartos, string nomeHotel)
    // HOTEL PODE MUDAR DE NOME, ENTAO AS REFERENCIAS PODEM SER PERDIDAS
    // GERAR QUARTOS DEVE SER RESPONSABILIDADE DO HOTEL, POIS OS QUARTOS SAO PROPRIEDADE DO HOTEL, E NAO DE UM SERVICO EXTERNO
    // CADA QUARTO DEVE POSSUIR UM ID DISTINTO< MESMO QUE SEJAM DE HOTEIS DIFERENTES
    {
        var _quartos = new List<Quarto>();

        double percentualPremium = 0.2; // 20% de quartos Premium
        int qtdPremium = (int)Math.Round(quantidadeDeQuartos * percentualPremium);
        int qtdStandard = quantidadeDeQuartos - qtdPremium;

        // Criação de quartos Standard
        for (int i = 1; i <= qtdStandard; i++)
        {
            Quarto quarto = new Quarto(i, "Standard", nomeHotel);
            _quartos.Add(quarto);
            _quartoRepositorio.Adicionar(quarto);
        }

        // Criação de quartos Premium
        for (int i = qtdStandard + 1; i <= quantidadeDeQuartos; i++)
        {
            Quarto quarto = new Quarto(i, "Premium", nomeHotel);
            _quartos.Add(quarto);
            _quartoRepositorio.Adicionar(quarto);
        }

        return _quartos;
    }
}
