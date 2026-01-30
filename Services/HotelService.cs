using System;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo gerenciamento de hotéis na aplicação.
/// </summary>
/// <remarks>
/// Atua como camada intermediária entre a interface do usuário (menus) e o <see cref="HotelRepositorio"/>.
/// Suas responsabilidades incluem:
/// - Validar dados de entrada para criação de hotéis.
/// - Gerar e criar instâncias de <see cref="Hotel"/> e <see cref="Quarto"/>.
/// - Delegar a persistência ao <see cref="HotelRepositorio"/>.
/// - Retornar mensagens de sucesso ou falha da operação.
/// 
/// Depende do <see cref="QuartoService"/> para geração de quartos e do <see cref="HotelRepositorio"/> para armazenamento.
/// </remarks>
internal class HotelService
{
    private readonly HotelRepositorio _hotelRepositorio;
    private readonly QuartoService _quartoService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="HotelService"/>.
    /// </summary>
    /// <param name="hotelRepositorio">Repositório que armazena os hotéis cadastrados.</param>
    /// <param name="quartoService">Serviço responsável por gerar quartos para o hotel.</param>
    public HotelService(HotelRepositorio hotelRepositorio, QuartoService quartoService)
    {
        _hotelRepositorio = hotelRepositorio;
        _quartoService = quartoService;
    }

    /// <summary>
    /// Cria um novo hotel, validando os dados de entrada.
    /// </summary>
    /// <param name="nome">Nome do hotel.</param>
    /// <param name="quantidadeDeQuartos">Quantidade de quartos como string (será convertida para inteiro).</param>
    /// <returns>
    /// Tupla contendo:
    /// - <c>sucesso</c>: indica se a operação foi concluída com êxito.
    /// - <c>mensagem</c>: feedback textual sobre o resultado da operação.
    /// </returns>
    /// <remarks>
    /// Validações aplicadas:
    /// - Nome do hotel não pode estar vazio.
    /// - Quantidade de quartos deve ser um número inteiro maior que zero.
    /// 
    /// Caso a validação seja bem-sucedida:
    /// - Os quartos são gerados automaticamente (aproximadamente 20% Premium e o restante Standard).
    /// - O hotel é criado e adicionado ao repositório.
    /// </remarks>
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

    /// <summary>
    /// Obtém todos os hotéis cadastrados.
    /// </summary>
    /// <returns>
    /// Lista somente leitura contendo todos os hotéis armazenados no <see cref="HotelRepositorio"/>.
    /// </returns>
    public IReadOnlyList<Hotel> ObterHoteis()
    {
        return _hotelRepositorio.ObterTodos();
    }
}
