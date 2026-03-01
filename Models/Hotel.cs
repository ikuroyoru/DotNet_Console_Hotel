using System.ComponentModel.DataAnnotations;

namespace DotNet_Console_Hotel.Models;

internal class Hotel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Zip_code { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int Room_Count { get; set; }
    public bool Is_active { get; set; } = true;

    // Construtor
    public Hotel(Guid id, string name, string street, int number,
                 string city, string state, string country,
                 string zip_code, string phone, int room_Count)
    {
        Id = id;
        Name = name;
        Street = street;
        Number = number;
        City = city;
        State = state;
        Country = country;
        Zip_code = zip_code;
        Phone = phone;
        Room_Count = room_Count;
        Is_active = true;
    }


    public List<Room> GenerateRooms(int singleCount, int doubleCount, int tripleCount, int suiteCount, decimal singlePrice, decimal doublePrice, decimal triplePrice, decimal suitePrice)
    {
        string[] categorias = { "Single", "Double", "Triple", "Suite" };

        List<Room> rooms = new List<Room>();
        int number = 1;

        for (int i = 0; i < singleCount; i++)
            rooms.Add(new Room(number++, singlePrice, "Single", Id));

        for (int i = 0; i < doubleCount; i++)
            rooms.Add(new Room(number++, doublePrice, "Double", Id));

        for (int i = 0; i < tripleCount; i++)
            rooms.Add(new Room(number++, triplePrice, "Triple", Id));

        for (int i = 0; i < suiteCount; i++)
            rooms.Add(new Room(number++, suitePrice, "Suite", Id));

        foreach (var room in rooms)
            Console.WriteLine($"Quarto {room.Number} - Hotel: {Id}");

        return rooms;
    }

}
