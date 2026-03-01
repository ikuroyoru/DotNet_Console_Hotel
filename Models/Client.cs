using System.ComponentModel.DataAnnotations;

namespace DotNet_Console_Hotel.Models;

internal class Client
{
    public Client(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;    
    }
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    // FALTA CARREGAR INFORMACOES ADICIONAIS DO CLIENTE (CPF, TELEFONE, ENDERECO, ETC) PARA O MOMENTO DE REALIZAR UMA RESERVA DE HOTEL
}
