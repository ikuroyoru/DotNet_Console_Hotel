namespace DotNet_Console_Hotel.Models;

internal class Reservation
{
    public Reservation(Guid client_Id, Guid room_Id, DateTime checkIn_Date, DateTime checkOut_Date)
    {
        CheckIn_Date = checkIn_Date;
        CheckOut_Date = checkOut_Date;
        Client_Id = client_Id;
        Room_Id = room_Id;
    }

    public Guid Id { get; set; }
    public Guid Client_Id { get; private set; }
    public Guid Room_Id { get; private set; }
    public DateTime CheckIn_Date { get; private set; }
    public DateTime CheckOut_Date { get; private set; }
}
