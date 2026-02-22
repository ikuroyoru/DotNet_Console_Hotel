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
            ProcessarOpcaoDeAutenticacao(escolha);
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
    private void ProcessarOpcaoDeAutenticacao(int escolha)
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

        string[] campos = {"Nome Completo", "E-Mail", "Senha", "Confirma Senha" };
        string nome = string.Empty;
        string email = string.Empty;
        string senha = string.Empty;

        int i = 0;

        while (i < campos.Length) // VALIDA CAMPOS ANTES DE CADASTRAR
        {
            Console.Write($"{campos[i]}: ");
            string input = Console.ReadLine()!;

            Result resultado = i switch
            {
                0 => AutenticacaoService.ValidaNome(input),
                1 => AutenticacaoService.ValidaFormatoEmail(input),
                2 => AutenticacaoService.ValidaSenha(input),
                3 => AutenticacaoService.ValidaConfirmaSenha(senha, input),
                _ => Result.Fail("Campo inválido")
            };

            if (resultado.Success)
            {
                switch (i)
                {
                    case 0: nome = input; break;
                    case 1: email = input; break;
                    case 2: senha = input; break;
                }

                i++;
                Console.Clear();
            }
            else
            {
                Console.WriteLine(resultado.Error);
                Console.WriteLine("Tente novamente.\n");
            }
        }

        var result = _autenticacaoService.Cadastrar(nome, email, senha);
        if (!result.Success)
        {
            Console.WriteLine(result.Error);
            Thread.Sleep(4000);
            Executar();
        }
        else
        {
            _autenticacaoService.Login(email, senha);
            _menuPrincipal.Executar();
        }

    }
    private void FormularioDeLogin()
    {
        Console.WriteLine("***** LOGIN *****");

        Console.Write("\nE-Mail: ");
        string email = Console.ReadLine()!;

        Console.Write("Senha: ");
        string senha = Console.ReadLine()!;

        var resultado = _autenticacaoService.Login(email, senha);

        if (!resultado.Success)
        {
            Console.WriteLine(resultado.Error);
            Console.WriteLine("\nPressione qualquer tecla para voltar ao Inicio");
            Console.ReadKey();
            Executar();
            return;
        }

        Console.WriteLine("Login efetuado com sucesso!");
        Thread.Sleep(3000);
        _menuPrincipal.Executar();
    }
}
