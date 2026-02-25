namespace DotNet_Console_Hotel.Models;

internal class Client
{
    public Client(Guid? id, string name, string email)
    {
        Name = name;
        Email = email;
        Id = id;
    }

    public Guid? Id { get; set; }
    public string Name { get; }
    public string Email { get; }

    // FALTA CARREGAR INFORMACOES ADICIONAIS DO CLIENTE (CPF, TELEFONE, ENDERECO, ETC) PARA O MOMENTO DE REALIZAR UMA RESERVA DE HOTEL
}
