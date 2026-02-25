using DotNet_Console_Hotel.Menus;
using DotNet_Console_Hotel.Infrastructure.Repositories;
using DotNet_Console_Hotel.Services;
using DotNet_Console_Hotel.Infrastructure.FileReaders;

var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=nihon";

var repositoryHotel = new RepositoryHotel(connectionString);
var repositoryRoom = new RepositoryRoom(connectionString);
var repositoryClient = new RepositoryClient(connectionString);
var repositoryReservation = new RepositoryReservation(connectionString);

CsvHotelImporter hotelReader = new(repositoryHotel, repositoryRoom);

var serviceSession = new ServiceSession();
var serviceRoom = new ServiceRoom(repositoryRoom);
var serviceHotel = new ServiceHotel(repositoryHotel, serviceRoom);
var serviceClient = new ServiceClient(repositoryClient);
var serviceReservation = new ServiceReservation(serviceSession, serviceRoom, repositoryReservation);
var serviceAuthentication = new ServiceAuthentication(serviceSession, repositoryClient);

var menu = new Menu();


var options = new Dictionary<int, Menu>
{
    { 1, new MenuCreateReservation(serviceHotel, serviceReservation) },
    { 2, new MenuShowHotels(serviceHotel) },
    { 3, new MenuShowPersonalInformation(repositoryClient, serviceSession)  }
    //{ 4, new MenuCreateReservation() } 
    //{ 5, new MenuCancelReservation() } 
    //{ 6, new MenuQuit() }
};

var menuMain = new MenuMain(options);
var menuAUthentication = new MenuAuthentication(serviceAuthentication, menuMain);


// reader.HotelReader();

menuAUthentication.Execute();

Console.ReadKey();


