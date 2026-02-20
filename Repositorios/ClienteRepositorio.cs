namespace DotNet_Console_Hotel.Repositorios;

using Npgsql;
using DotNet_Console_Hotel.Models;

internal class ClienteRepositorio
{
    public ClienteRepositorio(string connection)
    {
        connectionString = connection;
    }

    string connectionString;
    private readonly List<Cliente> Clientes = new(); // TROCAR PELO BANCO DE DADOS


    public Cliente? BuscarPorCpf(string cpf)
    {
        Cliente? cliente = Clientes.FirstOrDefault(c => c.Cpf == cpf);
        return cliente;
    }

    public void Adicionar(Cliente conta)
    {
        Clientes.Add(conta);
    }
}
