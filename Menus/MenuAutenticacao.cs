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

    /// <summary>
    /// Executa o fluxo principal do menu de autenticação.
    /// </summary>
    /// <remarks>
    /// Fluxo atual:
    /// - Exibe o menu base da classe <see cref="Menu"/>.
    /// - Mostra as opções de login ou criação de conta.
    /// - Valida a escolha do usuário.
    /// - Processa a opção escolhida chamando o formulário correspondente.
    /// - Loop contínuo até que o usuário efetue login ou cadastro com sucesso.
    /// </remarks>
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

    /// <summary>
    /// Exibe as opções disponíveis no menu de autenticação.
    /// </summary>
    private void ExibirOpcoesAutenticacao()
    {
        Console.WriteLine("***** BEM-VINDO AO HotelIdeal.com *****\n");

        for (int i = 0; i < _opcoesDeLogin.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_opcoesDeLogin[i]}");
        }
    }

    /// <summary>
    /// Valida a escolha do usuário no menu, garantindo que seja numérica e dentro do intervalo.
    /// </summary>
    /// <returns>Número da opção escolhida pelo usuário.</returns>
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

    /// <summary>
    /// Processa a opção escolhida pelo usuário no menu de autenticação.
    /// </summary>
    /// <param name="escolha">Número da opção escolhida.</param>
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

    /// <summary>
    /// Coleta informações do usuário e cria uma nova conta via <see cref="AutenticacaoService"/>.
    /// </summary>
    /// <remarks>
    /// Fluxo:
    /// - Solicita nome completo, CPF e senha.
    /// - Verifica se a senha e confirmação coincidem.
    /// - Chama <see cref="AutenticacaoService.Cadastrar"/> para criar a conta.
    /// - Exibe mensagem de sucesso ou erro.
    /// - Em caso de sucesso, direciona para o <see cref="MenuPrincipal"/>.
    /// </remarks>
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

    /// <summary>
    /// Coleta informações do usuário e realiza login via <see cref="AutenticacaoService"/>.
    /// </summary>
    /// <remarks>
    /// Fluxo:
    /// - Solicita CPF e senha.
    /// - Chama <see cref="AutenticacaoService.Login"/> para autenticação.
    /// - Exibe mensagem de sucesso ou erro.
    /// - Em caso de sucesso, direciona para o <see cref="MenuPrincipal"/>.
    /// </remarks>
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
