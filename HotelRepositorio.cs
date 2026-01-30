using System;

/// <summary>
/// Repositório responsável pelo armazenamento de instâncias de <see cref="Hotel"/>.
/// </summary>
/// <remarks>
/// Implementação baseada em memória.
/// Os dados são mantidos apenas durante a execução da aplicação.
/// Não há persistência em banco de dados ou arquivo.
/// </remarks>
internal class HotelRepositorio
{
    /// <summary>
    /// Lista interna que armazena os hotéis cadastrados.
    /// </summary>
    /// <remarks>
    /// A lista é pública e pode ser modificada diretamente.
    /// </remarks>
    public List<Hotel> Hoteis = new();

    /// <summary>
    /// Adiciona um novo hotel ao repositório.
    /// </summary>
    /// <param name="hotel">Instância do hotel a ser armazenada.</param>
    public void Adicionar(Hotel hotel)
    {
        Hoteis.Add(hotel);
    }

    /// <summary>
    /// Retorna todos os hotéis armazenados.
    /// </summary>
    /// <returns>
    /// Lista somente leitura contendo os hotéis cadastrados.
    /// </returns>
    /// <remarks>
    /// A lista retornada não pode ser modificada externamente,
    /// porém a lista interna do repositório continua mutável.
    /// </remarks>
    public IReadOnlyList<Hotel> ObterTodos()
    {
        return Hoteis.AsReadOnly();
    }
}
