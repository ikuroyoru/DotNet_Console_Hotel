namespace DotNet_Console_Hotel.Models;

internal class Quarto
{
    public Quarto(int numero, decimal preco,string categoria, Guid hotelId)
    {
        Numero = numero;
        Categoria = categoria;
        HotelId = hotelId;
        Preco = preco;
    }

    public int Numero { get; } // PARA QUARTOS DE UM MESMO HOTEL -> NUMEROS DIFERENTES, PARA QUARTOS DE HOTEIS DIFERENTES _> NUMEROS PODEM SE REPETIR, POIS O IDENTIFICADOR UNICO DO QUARTO SERA O ID GERADO AUTOMATICAMENTE, E NAO O NUMERO
    public string Categoria { get; }
    public decimal Preco { get; }
    public Guid HotelId { get; }

}
