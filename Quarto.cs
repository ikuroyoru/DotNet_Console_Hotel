/// <summary>
/// Representa um quarto pertencente a um hotel.
/// </summary>
/// <remarks>
/// Um quarto possui:
/// - Número identificador.
/// - Categoria (ex: Standard, Premium).
/// - Uma lista de reservas associadas.
/// 
/// A classe permite criar reservas, armazenando-as internamente.
/// Não há validação de conflito de datas nesta classe.
/// </remarks>
internal class Quarto
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Quarto"/>.
    /// </summary>
    /// <param name="numero">Número identificador do quarto.</param>
    /// <param name="categoria">Categoria do quarto.</param>
    public Quarto(int numero, string categoria)
    {
        Numero = numero;
        Categoria = categoria;
    }

    /// <summary>
    /// Número identificador do quarto.
    /// </summary>
    public int Numero { get; }

    /// <summary>
    /// Categoria do quarto (ex: Standard, Premium).
    /// </summary>
    public string Categoria { get; }

    /// <summary>
    /// Lista de reservas associadas ao quarto.
    /// </summary>
    /// <remarks>
    /// A lista é pública para leitura e modificação.
    /// Novas reservas são adicionadas por meio do método <see cref="CriarReserva"/>.
    /// </remarks>
    public List<Reserva> Reservas { get; set; } = new();

    /// <summary>
    /// Cria uma nova reserva para o quarto.
    /// </summary>
    /// <param name="dataEntrada">Data de entrada da reserva.</param>
    /// <param name="dataSaida">Data de saída da reserva.</param>
    /// <remarks>
    /// O método cria uma instância de <see cref="Reserva"/>
    /// e adiciona à lista de reservas do quarto.
    /// 
    /// Não há validação de datas ou verificação de conflito.
    /// A responsabilidade por validar disponibilidade
    /// está fora desta classe.
    /// </remarks>
    public void CriarReserva(DateTime dataEntrada, DateTime dataSaida)
    {
        Reserva reserva = new(dataEntrada, dataSaida);
        Reservas.Add(reserva);
    }
}
