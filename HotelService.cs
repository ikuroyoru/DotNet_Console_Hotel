using System;
using System.Reflection.Metadata.Ecma335;

/// <summary>
/// Serviço responsável por orquestrar operações relacionadas à entidade <see cref="Hotel"/>.
/// Atua como intermediário entre a camada de apresentação e o repositório.
/// </summary>
/// <remarks>
/// Responsabilidades:
/// - Validar dados de entrada antes da criação de um hotel.
/// - Criar instâncias de <see cref="Hotel"/>.
/// - Delegar persistência ao <see cref="HotelRepositorio"/>.
/// - Retornar mensagens indicando sucesso ou falha da operação.
/// </remarks>
internal class HotelService
{
    private readonly HotelRepositorio _hotelRepositorio;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="HotelService"/>.
    /// </summary>
    /// <param name="hotelRepositorio">
    /// Repositório responsável pelo armazenamento dos hotéis.
    /// </param>
    public HotelService(HotelRepositorio hotelRepositorio)
    {
        _hotelRepositorio = hotelRepositorio;
    }

    /// <summary>
    /// Cria um novo hotel após validar os dados informados.
    /// </summary>
    /// <param name="nome">Nome do hotel.</param>
    /// <param name="quantidadeDeQuartos">
    /// Quantidade de quartos informada como texto.
    /// </param>
    /// <returns>
    /// Uma tupla contendo:
    /// - sucesso: indica se a operação foi concluída com êxito.
    /// - mensagem: descrição do resultado da operação.
    /// </returns>
    /// <remarks>
    /// Regras aplicadas:
    /// - A quantidade de quartos deve ser um número inteiro válido.
    /// - A quantidade de quartos deve ser maior que zero.
    /// - O nome do hotel não pode ser vazio.
    /// - Em caso de sucesso, o hotel é criado e adicionado ao repositório.
    /// </remarks>
    public (bool sucesso, string mensagem) CriarHotel(string nome, string quantidadeDeQuartos)
    {
        if (!(int.TryParse(quantidadeDeQuartos, out int qtdQuartos)))
            return (false, "Entrada invalida: Quantidade de quartos deve ser um numero inteiro");

        if (qtdQuartos <= 0)
            return (false, "Um hotel deve possuir quartos");

        if (nome.Length <= 0)
            return (false, "Um hotel deve possuir um nome");

        Hotel hotel = new Hotel(nome, qtdQuartos);

        _hotelRepositorio.Adicionar(hotel);

        return (true, "Hotel " + hotel.Nome + " adicionado com sucesso");
    }

    /// <summary>
    /// Obtém a lista de todos os hotéis cadastrados.
    /// </summary>
    /// <returns>
    /// Lista somente leitura contendo os hotéis armazenados no repositório.
    /// </returns>
    public IReadOnlyList<Hotel> ObterHoteis()
    {
        return _hotelRepositorio.ObterTodos();
    }
}
