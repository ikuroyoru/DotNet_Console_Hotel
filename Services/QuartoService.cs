using System;
using System.Collections.Generic;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo gerenciamento de quartos.
/// </summary>
/// <remarks>
/// Atua como camada intermediária entre a aplicação (menus, serviços) e o <see cref="QuartoRepositorio"/>.
/// Responsabilidades:
/// - Criar quartos automaticamente com categorias Standard e Premium.
/// - Localizar quartos por número e hotel.
/// - Adicionar reservas aos quartos existentes.
/// - Lançar exceções ou retornar nulo quando o quarto não é encontrado.
/// </remarks>
internal class QuartoService
{
    private readonly QuartoRepositorio _quartoRepositorio;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="QuartoService"/>.
    /// </summary>
    /// <param name="quartoRepositorio">Repositório que armazena os quartos cadastrados.</param>
    public QuartoService(QuartoRepositorio quartoRepositorio)
    {
        _quartoRepositorio = quartoRepositorio;
    }

    /// <summary>
    /// Adiciona uma reserva a um quarto existente.
    /// </summary>
    /// <param name="reserva">Reserva que será associada ao quarto.</param>
    /// <param name="quarto">Instância do quarto que receberá a reserva.</param>
    /// <exception cref="Exception">Lançada quando o quarto especificado não é encontrado.</exception>
    /// <remarks>
    /// - O método adiciona a reserva à lista de reservas do quarto fornecido.
    /// - A validação de datas ou disponibilidade não é realizada neste método.
    /// </remarks>
    public void CriarReserva(Reserva reserva, Quarto quarto)
    {
        quarto.Reservas.Add(reserva);
    }

    /// <summary>
    /// Busca um quarto pelo número e pelo hotel.
    /// </summary>
    /// <param name="hotelId">Identificador do hotel ao qual o quarto pertence.</param>
    /// <param name="numero">Número do quarto a ser buscado.</param>
    /// <returns>
    /// Instância de <see cref="Quarto"/> correspondente, ou <c>null</c> se não encontrado.
    /// </returns>
    public Quarto? BuscarQuarto(string hotelId, int numero)
    {
        return _quartoRepositorio.ObterPorNumero(hotelId, numero);
    }

    /// <summary>
    /// Gera uma lista de quartos para um hotel.
    /// </summary>
    /// <param name="quantidadeDeQuartos">Número total de quartos a serem criados.</param>
    /// <param name="nomeHotel">Nome do hotel que receberá os quartos.</param>
    /// <returns>Lista de quartos criados.</returns>
    /// <remarks>
    /// - Aproximadamente 20% dos quartos são Premium e o restante Standard.
    /// - Os quartos são adicionados automaticamente ao <see cref="QuartoRepositorio"/>.
    /// - Os números dos quartos começam em 1 e são incrementados sequencialmente.
    /// </remarks>
    public List<Quarto> GerarQuartos(int quantidadeDeQuartos, string nomeHotel)
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
