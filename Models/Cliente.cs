namespace DotNet_Console_Hotel.Models;

internal class Cliente
{
    public Cliente(string nome, string cpf, string senha)
    {
        Nome = nome;
        Cpf = cpf; // CPF NAO PODE SER ID, IMPLEMENTAR UM ID ALEATORIO PARA O CLIENTE, POREM MANTER O CPF COMO UM CAMPO DE IDENTIFICACAO UNICO PARA BUSCA
        Senha = senha;
    }

    public string Nome { get; }

    public string Cpf { get; }

    public string Senha { get; private set; }

}
