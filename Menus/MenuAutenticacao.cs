namespace DotNet_Console_Hotel.Menus;

using DotNet_Console_Hotel.Services;

/// <summary>
/// Menu responsável por gerenciar o fluxo de autenticação do usuário.
/// </summary>
/// <remarks>
/// Este menu apresenta opções de login e cadastro de conta,
/// delegando a autenticação e criação de contas ao <see cref="AutenticacaoService"/>.
/// Após autenticação bem-sucedida, direciona para o <see cref="MenuPrincipal"/>.
/// 
/// Observações importantes:
/// - Todas as validações de login e cadastro (senhas, CPF, duplicidade) são feitas pelo serviço.
/// - O menu apenas coleta dados do usuário e exibe mensagens.
/// - Loop principal garante que o menu de autenticação seja exibido repetidamente
///   até que o usuário complete login ou cadastro.
/// </remarks>
internal class MenuAutenticacao : Menu
{
    private readonly AutenticacaoService _autenticacaoService;
    private readonly MenuPrincipal _menuPrincipal;
    private readonly List<string> _opcoesDeLogin = new()
    {
        "Login",
        "Criar uma Conta",
    };

    /// <summary>
    /// Inicializa uma nova instância de <see cref="MenuAutenticacao"/>.
    /// </summary>
    /// <param name="autenticacaoService">Serviço responsável por autenticar e cadastrar usuários.</param>
    /// <param name="menuPrincipal">Menu principal do sistema, chamado após login ou cadastro bem-sucedido.</param>
    public MenuAutenticacao(AutenticacaoService autenticacaoService, MenuPrincipal menuPrincipal)
    {
        _autenticacaoService = autenticacaoService;
        _menuPrincipal = menuPrincipal;
    }
    public override void Executar()
    {
        base.Executar();
        while (true)
        {
            ExibirOpcoesAutenticacao();
            int escolha = ValidaEscolha();
            ProcessarOpcaoDeLogin(escolha);
            Console.WriteLine("OPERACAO FINALIZADA");
        }
    }
    private void ExibirOpcoesAutenticacao()
    {
        Console.WriteLine("***** BEM-VINDO AO HotelIdeal.com *****\n");

        for (int i = 0; i < _opcoesDeLogin.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_opcoesDeLogin[i]}");
        }
    }
    private int ValidaEscolha()
    {
        bool opcaoValida = false;
        int escolha = 0;
        while (!opcaoValida)
        {
            Console.Write("Escolha uma opção: ");
            string escolhaEscrita = Console.ReadLine()!;

            if (!int.TryParse(escolhaEscrita, out int escolhaNumerica))
            {
                Console.WriteLine("Entrada inválida. Digite um número.");
                continue;
            }

            if (escolhaNumerica > _opcoesDeLogin.Count || escolhaNumerica < 1)
            {
                Console.WriteLine($"Opcao invalida. Tente novamente.");
                Console.WriteLine($"Escolheu {escolhaNumerica} de um maximo de {_opcoesDeLogin.Count}");
            }
            else
            {
                escolha = escolhaNumerica;
                opcaoValida = true;
                break;
            }
        }
        return escolha;
    }
    private void ProcessarOpcaoDeLogin(int escolha)
    {
        switch (escolha)
        {
            case 1:
                FormularioDeLogin();
                break;
            case 2:
                FormularioDeCadastro();
                break;
            default:
                Console.WriteLine("Opcao indisponivel no momento");
                break;
        }
    }
    private void FormularioDeCadastro()
    {
        Console.WriteLine("**** Criar uma conta ****");

        Console.Write("\nNome Completo: ");
        string nomeCompleto = Console.ReadLine()!;

        Console.Write("\nCPF: ");
        string cpf = Console.ReadLine()!;

        Console.Write("\nSenha: ");
        string senha = Console.ReadLine()!;

        Console.Write("\nConfirmar Senha: ");
        string confirmarSenha = Console.ReadLine()!;

        if (senha != confirmarSenha)
        {
            Console.WriteLine("\nAs senhas estao diferentes, tente novamente");
            Console.WriteLine("\nPressione Qualquer tecla para voltar ao Inicio");
            Console.ReadKey();
            ExibirOpcoesAutenticacao();
        }

        var resultado = _autenticacaoService.Cadastrar(nomeCompleto, cpf, senha);
        Console.WriteLine(resultado.mensagem);

        if (!resultado.sucesso)
        {
            Console.WriteLine("\nRetornando ao Inicio...");
            Thread.Sleep(3000);
            Executar();
            return;
        }

        Thread.Sleep(3000);
        _menuPrincipal.Executar();
    }
    private void FormularioDeLogin()
    {
        Console.WriteLine("***** LOGIN *****");

        Console.Write("\nCPF: ");
        string cpf = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = Console.ReadLine()!;

        var resultado = _autenticacaoService.Login(cpf, senha);

        if (!resultado.sucesso)
        {
            Console.WriteLine(resultado.mensagem);
            Console.WriteLine("\nPressione qualquer tecla para voltar ao Inicio");
            Console.ReadKey();
            Executar();
            return;
        }

        Console.WriteLine(resultado.mensagem);
        Thread.Sleep(2000);
        _menuPrincipal.Executar();
    }
}
