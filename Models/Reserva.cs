namespace DotNet_Console_Hotel.Models;

internal class Reserva
{
    public Reserva(DateTime dataEntrada, DateTime dataSaida, string userId, int quartoId)
    {
        DataEntrada = dataEntrada;
        DataSaida = dataSaida;
        UserId = userId;
        QuartoId = quartoId;
    }

    public string UserId { get; }
    public int QuartoId { get; }
    public DateTime DataEntrada { get; private set; }
    public DateTime DataSaida { get; private set; }
}
