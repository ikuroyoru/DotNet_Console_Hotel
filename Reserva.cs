internal class Reserva
{
    public Reserva(DateTime dataEntrada, DateTime dataSaida)
    {
        DataEntrada = dataEntrada;
        DataSaida = dataSaida;
    }

    // Cliente Cliente { get; set; }
    /* string Status; */ // ativo / cancelado / reembolsado
    public DateTime DataEntrada {  get; private set; }
    public DateTime DataSaida { get; private set; }

}