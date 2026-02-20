using System;
using System.Collections.Generic;
using System.Text;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;

namespace DotNet_Console_Hotel;

internal class csvReader
{
    public csvReader(HotelRepositorio hotelRepositorio, QuartoRepositorio quartoRepositorio)
    {
        _hotelRepositorio = hotelRepositorio;
        _quartoRepositorio = quartoRepositorio;
    }

    HotelRepositorio _hotelRepositorio;
    QuartoRepositorio _quartoRepositorio;

    public void HotelReader()
    {
        string caminhoArquivo = @"C:\Users\WorkG1\Desktop\hotel.csv"; // caminho do arquivo CSV


        using (StreamReader leitor = new StreamReader(caminhoArquivo))
        {
            string cabecalho = leitor.ReadLine()!; // cabeçalho

            string? linha;

            while ((linha = leitor.ReadLine()) != null)
            {
                string[] campos = linha.Split(','); // divide em colunas

                if (campos.Length < 17)
                    continue; // linha incompleta

                if (!int.TryParse(campos[2], out int numero))
                    continue;

                string nome = campos[0].Trim();
                string rua = campos[1].Trim();
                string cidade = campos[3].Trim();
                string estado = campos[4].Trim();
                string pais = campos[5].Trim();
                string cep = campos[6].Trim();
                string telefone = campos[7].Trim();

                if (string.IsNullOrWhiteSpace(nome) ||
                    string.IsNullOrWhiteSpace(rua) ||
                    string.IsNullOrWhiteSpace(cidade) ||
                    string.IsNullOrWhiteSpace(estado) ||
                    string.IsNullOrWhiteSpace(pais) ||
                    string.IsNullOrWhiteSpace(cep) ||
                    string.IsNullOrWhiteSpace(telefone)
                   )
                {
                    continue; // linha inválida
                }

                if (!int.TryParse(campos[8], out int quantidade_quartos))
                    continue;

                if (!int.TryParse(campos[9], out int quantidade_single))
                    continue;

                if (!int.TryParse(campos[10], out int quantidade_double))
                    continue;

                if (!int.TryParse(campos[11], out int quantidade_triple))
                    continue;

                if (!int.TryParse(campos[12], out int quantidade_suite))
                    continue;

                if (!decimal.TryParse(campos[13], out decimal precoSingle))
                    continue;

                if (!decimal.TryParse(campos[14], out decimal precoDouble))
                    continue;

                if (!decimal.TryParse(campos[15], out decimal precoTriple))
                    continue;

                if (!decimal.TryParse(campos[16], out decimal precoSuite))
                    continue;

                Hotel hotel = new Hotel(
                    nome,
                    rua,
                    numero,
                    cidade,
                    estado,
                    pais,
                    cep,
                    telefone,
                    quantidade_quartos
                    );

                Hotel? hotelComId = _hotelRepositorio.AdicionarHoteis(hotel);

                if (hotelComId != null)
                {
                    // Console.WriteLine($"{hotelComId.Nome}, {hotelComId.Numero}, {hotelComId.Pais}, {hotelComId.Estado}, {hotelComId.Cidade}, {hotelComId.Cep}, {hotelComId.Ativo}");

                    List<Quarto> quartos = hotelComId.GerarQuartos(quantidade_single, quantidade_double, quantidade_triple, quantidade_suite, precoSingle, precoDouble, precoTriple, precoSuite);
                    _quartoRepositorio.AdicionarQuartos(quartos);
                    Console.WriteLine();
                }

            }
        }
    }
}
