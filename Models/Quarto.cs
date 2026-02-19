namespace DotNet_Console_Hotel.Models;

/// <summary>
/// Representa um quarto pertencente a um hotel.
/// </summary>
/// <remarks>
/// Um quarto possui:
/// - Número identificador.
/// - Categoria (ex: Standard, Premium).
/// - Identificador do hotel ao qual pertence (<see cref="HotelId"/>).
/// - Uma lista de reservas associadas.
///
/// A classe armazena reservas, mas **não realiza validação de disponibilidade ou conflito de datas**.
/// A responsabilidade de validação e gerenciamento de disponibilidade deve ser feita externamente,
/// geralmente via <see cref="ReservaService"/>.
/// </remarks>
internal class Quarto
{
    /// <summary>
    /// Inicializa uma nova instância de <see cref="Quarto"/>.
    /// </summary>
    /// <param name="numero">Número identificador do quarto.</param>
    /// <param name="categoria">Categoria do quarto.</param>
    /// <param name="hotelId">Identificador do hotel ao qual o quarto pertence.</param>
    public Quarto(int numero, string categoria, string hotelId)
    {
        Numero = numero;
        Categoria = categoria;
        HotelId = hotelId;
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
    /// Identificador do hotel ao qual o quarto pertence.
    /// </summary>
    public string HotelId { get; }

    /// <summary>
    /// Lista de reservas associadas ao quarto.
    /// </summary>
    /// <remarks>
    /// A lista é pública para leitura e modificação.
    /// Novas reservas podem ser adicionadas por meio do método <see cref="AdicionarReserva"/>.
    /// </remarks>
    public List<Reserva> Reservas { get; private set; } = new();

    /// <summary>
    /// Adiciona uma reserva à lista de reservas do quarto.
    /// </summary>
    /// <param name="reserva">Reserva a ser adicionada.</param>
    /// <remarks>
    /// Cria uma instância de <see cref="Reserva"/> (ou recebe uma existente)
    /// e adiciona à lista interna.
    /// 
    /// Não há verificação de conflito de datas; a responsabilidade de validar
    /// a disponibilidade é externa.
    /// </remarks>
    public void AdicionarReserva(Reserva reserva)
    {
        Reservas.Add(reserva);
    }
}
