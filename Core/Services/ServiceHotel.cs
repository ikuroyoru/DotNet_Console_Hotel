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

    public IReadOnlyList<Hotel> GetHotels(int hotelLoadCount)
    {
        return _hotelRepository.GetHotels(hotelLoadCount);
    }
}
