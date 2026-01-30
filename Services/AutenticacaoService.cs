using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

/// <summary>
/// Serviço responsável pelo cadastro e autenticação de clientes.
/// </summary>
/// <remarks>
/// Este serviço interage com:
/// - <see cref="ClienteService"/> para criação e busca de clientes.
/// - <see cref="SessaoService"/> para gerenciamento da sessão do usuário logado.
///
/// Funcionalidades principais:
/// - Normalização, validação e padronização de CPF.
/// - Criação de contas de cliente.
/// - Login e inicialização de sessão.
/// </remarks>
internal class AutenticacaoService
{
    private readonly ClienteService _clienteService;
    private readonly SessaoService _sessaoService;

    /// <summary>
    /// Inicializa uma nova instância de <see cref="AutenticacaoService"/>.
    /// </summary>
    /// <param name="clienteService">Serviço responsável pelo gerenciamento de clientes.</param>
    /// <param name="sessaoService">Serviço responsável pelo gerenciamento da sessão de usuários.</param>
    public AutenticacaoService(ClienteService clienteService, SessaoService sessaoService)
    {
        _clienteService = clienteService;
        _sessaoService = sessaoService;
    }

    /// <summary>
    /// Cadastra um novo cliente no sistema.
    /// </summary>
    /// <param name="nome">Nome completo do cliente.</param>
    /// <param name="cpf">CPF do cliente (qualquer formato).</param>
    /// <param name="senha">Senha escolhida pelo cliente.</param>
    /// <returns>
    /// Tupla contendo:
    /// - sucesso: indica se a operação foi concluída com êxito.
    /// - mensagem: feedback textual sobre a operação.
    /// </returns>
    public (bool sucesso, string mensagem) Cadastrar(string nome, string cpf, string senha)
    {
        string CpfNormalizado = NormalizarCpf(cpf);
        var cpfValido = ValidaCpf(CpfNormalizado);

        if (!cpfValido.sucesso)
            return (cpfValido.sucesso, "Houve um erro ao criar a conta: CPF invalido");

        CpfNormalizado = PadronizarCpf(CpfNormalizado);

        _clienteService.AdicionarCliente(nome, CpfNormalizado, senha);

        var resultado = Login(cpf, senha);
        return (resultado.sucesso, "Conta criada com sucesso! Seja Bem-vindo(a) " + nome);
    }

    /// <summary>
    /// Realiza o login de um cliente existente.
    /// </summary>
    /// <param name="cpf">CPF do cliente (qualquer formato).</param>
    /// <param name="senha">Senha do cliente.</param>
    /// <returns>
    /// Tupla contendo:
    /// - sucesso: indica se o login foi bem-sucedido.
    /// - mensagem: feedback textual sobre a operação.
    /// </returns>
    public (bool sucesso, string mensagem) Login(string cpf, string senha)
    {
        string cpfNormalizado = NormalizarCpf(cpf);
        var cpfValido = ValidaCpf(cpfNormalizado);
        if (!cpfValido.sucesso)
        {
            return (false, "CPF Invalido, tente novamente.");
        }

        string cpfPadronizado = PadronizarCpf(cpfNormalizado);

        Cliente? _usuario = _clienteService.BuscarCliente(cpfPadronizado);

        if (_usuario == null)
        {
            return (false, "A conta informada nao existe. CPF: " + cpfPadronizado);
        }
        if (senha != _usuario.Senha)
        {
            return (false, "O CPF ou senha estao incorretos. Tente novamente.");
        }

        _sessaoService.IniciarSessao(_usuario.Cpf);
        return (true, "Seja Bem-Vindo(a) " + _usuario.Nome);
    }

    /// <summary>
    /// Remove caracteres não numéricos do CPF.
    /// </summary>
    /// <param name="cpf">CPF a ser normalizado.</param>
    /// <returns>CPF contendo apenas números.</returns>
    private string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }

    /// <summary>
    /// Valida se o CPF possui 11 dígitos numéricos.
    /// </summary>
    /// <param name="cpf">CPF normalizado (apenas números).</param>
    /// <returns>
    /// Tupla indicando:
    /// - sucesso: verdadeiro se o CPF é válido.
    /// - mensagem: feedback textual sobre a validação.
    /// </returns>
    private (bool sucesso, string mensagem) ValidaCpf(string cpf)
    {
        if (!cpf.All(char.IsDigit))
        {
            return (false, "CPF deve ter apenas numeros");
        }
        if (cpf.Length != 11)
        {
            return (false, "CPF deve ter 11 digitos");
        }
        return (true, "O CPF " + cpf + " e valido");
    }

    /// <summary>
    /// Padroniza o CPF no formato "000.000.000-00".
    /// </summary>
    /// <param name="cpf">CPF contendo apenas números.</param>
    /// <returns>CPF formatado.</returns>
    private string PadronizarCpf(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }
}
