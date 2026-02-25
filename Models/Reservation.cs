namespace DotNet_Console_Hotel.Models;

internal class Reservation
{
    public Reservation(DateTime checkInDate, DateTime checkOutDate, string clientId, int quartoId)
    {
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        ClientId = clientId;
        RoomId = quartoId;
    }

    public string ClientId { get; }
    public int RoomId { get; }
    public DateTime CheckInDate { get; private set; }
    public DateTime CheckOutDate { get; private set; }
}
