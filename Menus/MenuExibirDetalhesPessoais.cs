using DotNet_Console_Hotel.Repositorios;
using DotNet_Console_Hotel.Services;
namespace DotNet_Console_Hotel.Menus;


internal class MenuExibirDetalhesPessoais : Menu
{
    public MenuExibirDetalhesPessoais(ClienteRepositorio clienteRepositorio, SessaoService sessaoService)
    {
        _clienteRepositorio = clienteRepositorio;
        _sessaoService = sessaoService;
    }

    private readonly ClienteRepositorio _clienteRepositorio;
    private readonly SessaoService _sessaoService;

    public override void Executar()
    {
        var cliente = _clienteRepositorio.BuscarClientePorId(_sessaoService.ObterIdUsuarioConectado());

        if (cliente == null)
        {
            Console.WriteLine("Cliente não encontrado: Usuario desconectado. Conecte-se para usar essa funcao");
            return;
        }

        Console.WriteLine("***** SEUS DETALHES PESSOAIS *****\n");
        Console.WriteLine($"ID: {cliente.Id}");
        Console.WriteLine($"Nome: {cliente.Nome}");
        Console.WriteLine($"Email: {cliente.Email}");
    }
}
