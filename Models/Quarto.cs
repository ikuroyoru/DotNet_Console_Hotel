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
        id = numero;
        Numero = numero;
        Categoria = categoria;
        HotelId = hotelId;
    }

    public int id { get; } // IDENTIFICADOR UNICO DO QUARTO, GERADO AUTOMATICAMENTE, PARA GARANTIR QUE MESMO QUARTOS DE HOTEIS DIFERENTES TENHAM IDENTIFICADORES DISTINTOS
    public int Numero { get; } // PARA QUARTOS DE UM MESMO HOTEL -> NUMEROS DIFERENTES, PARA QUARTOS DE HOTEIS DIFERENTES _> NUMEROS PODEM SE REPETIR, POIS O IDENTIFICADOR UNICO DO QUARTO SERA O ID GERADO AUTOMATICAMENTE, E NAO O NUMERO
    public string Categoria { get; }
    public string HotelId { get; }
    public List<Reserva> Reservas { get; private set; } = new(); // ISSO NAO DEVE MAIS EXISTIR, POIS RESERVAS SAO REFERENCIADAS POR ID

}
