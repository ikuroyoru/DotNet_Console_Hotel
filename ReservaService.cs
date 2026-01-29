namespace DotNet_Console_Hotel.Services;

internal class ReservaService
{
    public ReservaService() { }


    public (string mensagem, bool sucesso) AtribuirQuarto(Hotel hotel, DateTime dataEntrada, DateTime dataSaida)
    {
        if (dataEntrada == default || dataSaida == default)
        {
            return ("As datas nao podem ser vazias.", false);
        }

        if (dataSaida <= dataEntrada)
        {
            return ("A data de saida deve ser posterior à data de entrada.", false);
        }

        var quartoDisponivel = hotel.Quartos
        .FirstOrDefault(quarto =>
            !quarto.Reservas.Any(reserva =>
                dataEntrada < reserva.DataSaida &&
                dataSaida > reserva.DataEntrada
            )
        );


        if (quartoDisponivel == null)
        {
            return ($"Nao ha quartos disponiveis para o periodo {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}. Por favor, selecione outra data.", false);
        }

        quartoDisponivel.CriarReserva(dataEntrada, dataSaida);
        return ($"O quarto {quartoDisponivel.Numero} foi reservado com sucesso para {dataEntrada:yyyy-MM-dd} a {dataSaida:yyyy-MM-dd}.", true);
    }
}



