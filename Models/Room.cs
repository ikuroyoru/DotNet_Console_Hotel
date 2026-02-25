namespace DotNet_Console_Hotel.Models;

internal class Room
{
    public Room(int numero, decimal preco,string categoria, Guid hotelId)
    {
        Number = numero;
        Category = categoria;
        HotelId = hotelId;
        Price = preco;
    }

    public int Number { get; } // PARA QUARTOS DE UM MESMO HOTEL -> NUMEROS DIFERENTES, PARA QUARTOS DE HOTEIS DIFERENTES _> NUMEROS PODEM SE REPETIR, POIS O IDENTIFICADOR UNICO DO QUARTO SERA O ID GERADO AUTOMATICAMENTE, E NAO O NUMERO
    public string Category { get; }
    public decimal Price { get; }
    public Guid HotelId { get; }

}
