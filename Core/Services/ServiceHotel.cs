using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Infrastructure.Repositories;

namespace DotNet_Console_Hotel.Services;

internal class ServiceHotel
{
    private readonly RepositoryHotel _hotelRepository;

    public ServiceHotel(RepositoryHotel hotelRepository, ServiceRoom roomService)
    {
        _hotelRepository = hotelRepository;
    }

    public void CreateHotel()
    {
        string pathFile = @"C:\Path\For\File.csv";

        using (StreamReader reader = new StreamReader(pathFile))
        {
            string content = reader.ReadToEnd();  // read all content of the file into a string
            Console.WriteLine(content);
        }
    }

    public IReadOnlyList<Hotel> GetHotels(int hotelLoadCount)
    {
        return _hotelRepository.LoadHotels(hotelLoadCount);
    }
}
