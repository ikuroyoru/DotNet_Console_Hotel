internal class Quarto
{
    public Quarto(int numero, string categoria)
    {
        Numero = numero;
        Categoria = categoria;
    }

    public int Numero { get; }
    public string Categoria { get; }
    public List<Reserva> Reservas { get; set; } = new(); // CRIAR OBJETO DE RESERVA

    public void CriarReserva(/* string nomeCliente*/ DateTime dataEntrada, DateTime dataSaida)
    {
        Reserva reserva = new(dataEntrada, dataSaida);
        Reservas.Add(reserva);
    }
}