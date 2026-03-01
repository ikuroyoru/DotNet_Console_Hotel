namespace DotNet_Console_Hotel.Models;

internal class Room
{
    public Room(int number, decimal price, string category, Guid hotel_Id)
    {
        Number = number;
        Category = category;
        Hotel_Id = hotel_Id;
        Price = price;
    }

    public Guid Id { get; private set; }
    public int Number { get; set; } // PARA QUARTOS DE UM MESMO HOTEL -> NUMEROS DIFERENTES, PARA QUARTOS DE HOTEIS DIFERENTES _> NUMEROS PODEM SE REPETIR, POIS O IDENTIFICADOR UNICO DO QUARTO SERA O ID GERADO AUTOMATICAMENTE, E NAO O NUMERO
    public string Category { get; set; }
    public decimal Price { get; private set; }
    public Guid Hotel_Id { get; private set; }

}
