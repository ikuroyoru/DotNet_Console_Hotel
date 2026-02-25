using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Models;

namespace DotNet_Console_Hotel.Infrastructure.FileReaders;

internal class CsvHotelImporter
{
    public CsvHotelImporter(RepositoryHotel repositoryHotel, RepositoryRoom repositoryRoom)
    {
        _repositoryHotel = repositoryHotel;
        _repositoryRoom = repositoryRoom;
    }

    RepositoryHotel _repositoryHotel;
    RepositoryRoom _repositoryRoom;

    public void HotelReader()
    {
        string filePath = @"C:\Users\WorkG1\Desktop\hotel.csv"; // csv file path


        using (StreamReader reader = new StreamReader(filePath))
        {
            string header = reader.ReadLine()!; // Header

            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] fields = line.Split(',');

                if (fields.Length < 17)
                    continue; // invalid line

                if (!int.TryParse(fields[2], out int number))
                    continue;

                string name = fields[0].Trim();
                string street = fields[1].Trim();
                string city = fields[3].Trim();
                string state = fields[4].Trim();
                string country = fields[5].Trim();
                string zipcode = fields[6].Trim();
                string telephone = fields[7].Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(street) ||
                    string.IsNullOrWhiteSpace(city) ||
                    string.IsNullOrWhiteSpace(state) ||
                    string.IsNullOrWhiteSpace(country) ||
                    string.IsNullOrWhiteSpace(zipcode) ||
                    string.IsNullOrWhiteSpace(telephone)
                   )
                {
                    continue; // invalid line
                }

                if (!int.TryParse(fields[8], out int allRoomCount))
                    continue;

                if (!int.TryParse(fields[9], out int singleRoomCount))
                    continue;

                if (!int.TryParse(fields[10], out int doubleRoomCount))
                    continue;

                if (!int.TryParse(fields[11], out int tripleRoomCount))
                    continue;

                if (!int.TryParse(fields[12], out int suiteRoomCount))
                    continue;

                if (!decimal.TryParse(fields[13], out decimal singleRoomPrice))
                    continue;

                if (!decimal.TryParse(fields[14], out decimal doubleRoomPrice))
                    continue;

                if (!decimal.TryParse(fields[15], out decimal tripleRoomPrice))
                    continue;

                if (!decimal.TryParse(fields[16], out decimal suiteRoomPrice))
                    continue;

                Hotel hotel = new Hotel(
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

                Hotel? hotelWithId = _repositoryHotel.AddHotel(hotel);

                if (hotelWithId != null)
                {
                    List<Room> rooms = hotelWithId.GenerateRooms(singleRoomCount, doubleRoomCount, tripleRoomCount, suiteRoomCount, singleRoomPrice, doubleRoomPrice, tripleRoomPrice, suiteRoomPrice);
                    _repositoryRoom.AddRoom(rooms);
                    Console.WriteLine();
                }

            }
        }
    }
}
