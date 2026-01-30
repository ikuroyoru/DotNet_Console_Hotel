using System;
using DotNet_Console_Hotel.Models;

/// <summary>
/// Repositório responsável pelo armazenamento de instâncias de <see cref="Quarto"/>.
/// </summary>
/// <remarks>
/// Implementação baseada em memória.
/// Os dados são mantidos apenas durante a execução da aplicação.
/// Não há persistência em banco de dados ou arquivo.
/// </remarks>
internal class QuartoRepositorio
{
    /// <summary>
    /// Lista interna que armazena os quartos cadastrados.
    /// </summary>
    /// <remarks>
    /// A lista é pública, mas idealmente apenas leitura deve ser exposta.
    /// A modificação direta deve ser evitada fora desta classe.
    /// </remarks>
    public List<Quarto> Quartos = new();

    /// <summary>
    /// Adiciona um novo quarto ao repositório.
    /// </summary>
    /// <param name="quarto">Instância do quarto a ser armazenada.</param>
    public void Adicionar(Quarto quarto)
    {
        Quartos.Add(quarto);
    }

    /// <summary>
    /// Retorna todos os quartos armazenados.
    /// </summary>
    /// <returns>Lista somente leitura contendo os quartos cadastrados.</returns>
    public IReadOnlyList<Quarto> ObterTodos()
    {
        return Quartos.AsReadOnly();
    }

    /// <summary>
    /// Busca um quarto pelo número identificador.
    /// </summary>
    /// <param name="numero">Número do quarto a ser buscado.</param>
    /// <returns>
    /// A instância de <see cref="Quarto"/> correspondente ao número,
    /// ou null se não encontrado.
    /// </returns>
    public Quarto? ObterPorNumero(string hotelId, int numero)
    {
        return Quartos.Find(q => q.Numero == numero && q.HotelId == hotelId);
    }
}
