namespace DotNet_Console_Hotel.Models;

internal class Hotel
{
    public Hotel(string name, string street, int number, string city, string state, string country, string zipcode, string telephone, int roomCount)
    {
        this.Name = name;
        this.Street = street;
        this.Number = number;
        this.City = city;
        this.State = state;
        this.Zipcode = zipcode;
        this.Country = country;
        this.Telephone = telephone;
        this.RoomCount = roomCount;
    }

    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string City { get; }
    public string State { get; }
    public string Zipcode { get; private set; }
    public string Country { get; }
    public string Telephone { get; private set; }
    public int RoomCount { get; private set; }
    public bool Active { get; private set; }

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
