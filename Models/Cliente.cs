namespace DotNet_Console_Hotel.Models;

internal class Cliente
{
    public Cliente(Guid? id, string nome, string email)
    {
        Nome = nome;
        Email = email;
        Id = id;
    }

    public Guid? Id { get; set; }
    public string Nome { get; }
    public string Email { get; }

    // FALTA CARREGAR INFORMACOES ADICIONAIS DO CLIENTE (CPF, TELEFONE, ENDERECO, ETC) PARA O MOMENTO DE REALIZAR UMA RESERVA DE HOTEL
}
