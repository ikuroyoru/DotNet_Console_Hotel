internal class Quarto
{
    public Quarto(int numero, string categoria)
    {
        Numero = numero;
        Categoria = categoria;
    }

    public int Numero { get; }
    public string Categoria { get; }
    // List<Reserva> Reserva { get; set; } // CRIAR OBJETO DE RESERVA

    void CriarReserva()
    {
        // CRIAR REGRA DE NEGOCIOS PARA RESERVAS - VALIDAR DATAS ANTES DE CRIAR RESERVAS
    }
}