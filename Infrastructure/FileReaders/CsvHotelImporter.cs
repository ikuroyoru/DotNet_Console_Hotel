using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Infrastructure.FileReaders;

internal class CsvHotelImporter
{
    private readonly RepositoryHotel _repositoryHotel;
    private readonly RepositoryRoom _repositoryRoom;

    public CsvHotelImporter(RepositoryHotel repositoryHotel, RepositoryRoom repositoryRoom)
    {
        _repositoryHotel = repositoryHotel;
        _repositoryRoom = repositoryRoom;
    }

    public void HotelReader()
    // MUST USE ROOM SERVICE TO ADD THE GENERATED ROOMS
    // MUST USE ROOM SERVICE TO ADD THE GENERATED HOTELS
    {
        string filePath = @"C:\Users\WorkG1\Desktop\hotel.csv";

        using var reader = new StreamReader(filePath);
        string header = reader.ReadLine()!; // header

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] fields = line.Split(',');
            if (fields.Length < 17) continue; // invalid line

            // Parse seguro de inteiros e decimais
            if (!int.TryParse(fields[2], out int number)) continue;
            if (!int.TryParse(fields[8], out int allRoomCount)) continue;
            if (!int.TryParse(fields[9], out int singleRoomCount)) continue;
            if (!int.TryParse(fields[10], out int doubleRoomCount)) continue;
            if (!int.TryParse(fields[11], out int tripleRoomCount)) continue;
            if (!int.TryParse(fields[12], out int suiteRoomCount)) continue;
            if (!decimal.TryParse(fields[13], out decimal singleRoomPrice)) continue;
            if (!decimal.TryParse(fields[14], out decimal doubleRoomPrice)) continue;
            if (!decimal.TryParse(fields[15], out decimal tripleRoomPrice)) continue;
            if (!decimal.TryParse(fields[16], out decimal suiteRoomPrice)) continue;

            // must fields are filled with default values if they're empty
            string name = string.IsNullOrWhiteSpace(fields[0]) ? "Unknown Hotel" : fields[0].Trim();
            string street = string.IsNullOrWhiteSpace(fields[1]) ? "Unknown Street" : fields[1].Trim();
            string city = string.IsNullOrWhiteSpace(fields[3]) ? "Unknown City" : fields[3].Trim();
            string state = string.IsNullOrWhiteSpace(fields[4]) ? "Unknown State" : fields[4].Trim();
            string country = string.IsNullOrWhiteSpace(fields[5]) ? "Unknown Country" : fields[5].Trim();
            string zipcode = string.IsNullOrWhiteSpace(fields[6]) ? "00000-000" : fields[6].Trim();
            string telephone = string.IsNullOrWhiteSpace(fields[7]) ? "000000000" : fields[7].Trim();

            Hotel hotel = new Hotel(
                Guid.Empty,
                name,
                street,
                number,
                city,
                state,
                country,
                zipcode,
                telephone,
                allRoomCount
            );

            // Adiciona via repositório (EF Core) de forma segura
            Hotel? hotelWithId = _repositoryHotel.AddHotel(hotel);

            if (hotelWithId != null)
            {
                // Gera os quartos com preços e quantidades
                List<Room> rooms = hotelWithId.GenerateRooms(
                    singleRoomCount, doubleRoomCount, tripleRoomCount, suiteRoomCount,
                    singleRoomPrice, doubleRoomPrice, tripleRoomPrice, suiteRoomPrice
                );

                _repositoryRoom.AddGeneratedRooms(rooms);
                Console.WriteLine($"Hotel '{hotelWithId.Name}' imported successfully!");
            }
            else
            {
                Console.WriteLine($"Hotel '{hotel.Name}' already exists.");
            }
        }
    }

    // COMMIT PARA CENTRALIZAR A GERACAO DE HOTEIS E QUARTOS NOS DEVIDOS SERVICES
}