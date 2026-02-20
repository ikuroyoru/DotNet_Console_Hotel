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

    public void CriarHotel()
    {
        string caminhoArquivo = @"C:\caminho\para\arquivo.csv";

        using (StreamReader leitor = new StreamReader(caminhoArquivo))
        {
            string conteudo = leitor.ReadToEnd();  // lê todo o arquivo de uma vez
            Console.WriteLine(conteudo);
        }
    }

    public IReadOnlyList<Hotel> ObterHoteis()
    {
        return _hotelRepositorio.ObterTodos();
    }
}
