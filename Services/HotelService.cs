using System;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Repositorios;

internal class HotelService
{
    private readonly HotelRepositorio _hotelRepositorio;
    private readonly QuartoService _quartoService;

    public HotelService(HotelRepositorio hotelRepositorio, QuartoService quartoService)
    {
        _hotelRepositorio = hotelRepositorio;
        _quartoService = quartoService;
    }

    public (bool sucesso, string mensagem) CriarHotel(string nome, string quantidadeDeQuartos)
    {
        if (!(int.TryParse(quantidadeDeQuartos, out int qtdQuartos)))
            return (false, "Entrada inválida: Quantidade de quartos deve ser um número inteiro.");

        if (qtdQuartos <= 0)
            return (false, "Um hotel deve possuir ao menos um quarto.");

        if (string.IsNullOrWhiteSpace(nome))
            return (false, "Um hotel deve possuir um nome válido.");

        List<Quarto> quartos = _quartoService.GerarQuartos(qtdQuartos, nome);

        Hotel hotel = new Hotel(nome, quartos);

        _hotelRepositorio.Adicionar(hotel);

        return (true, $"Hotel {hotel.Nome} criado com sucesso.");
    }

    public IReadOnlyList<Hotel> ObterHoteis()
    {
        return _hotelRepositorio.ObterTodos();
    }
}
