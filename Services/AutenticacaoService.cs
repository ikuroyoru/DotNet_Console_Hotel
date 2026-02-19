using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DotNet_Console_Hotel.Services;

internal class AutenticacaoService
{
    private readonly ClienteService _clienteService;
    private readonly SessaoService _sessaoService;

    public AutenticacaoService(SessaoService sessaoService, ClienteService clienteService)
    {
        _clienteService = clienteService;
        _sessaoService = sessaoService;
    }

    public (bool sucesso, string mensagem) Cadastrar(string nome, string cpf, string senha)
    {
        string CpfNormalizado = NormalizarCpf(cpf);
        var validacaoCpf = ValidaCpf(CpfNormalizado);

        if (!validacaoCpf.valido)
            return (false, "Houve um erro ao criar a conta: CPF invalido");

        var CpfPadronizado = PadronizarCpf(CpfNormalizado);

        _clienteService.AdicionarCliente(nome, CpfPadronizado, senha);

        var resultado = Login(cpf, senha);
        return (resultado.sucesso, "Conta criada com sucesso! Seja Bem-vindo(a) " + nome);
    }

    public (bool sucesso, string mensagem) Login(string cpf, string senha)
    {
        var validacaoCpf = ValidaCpf(cpf);
        var cliente = _clienteService.BuscarCliente(validacaoCpf.cpfPadronizado);

        if ((cliente == null) || (senha != cliente.Senha))
            return (false, "O CPF ou senha estao incorretos. Tente novamente.");

        _sessaoService.IniciarSessao(cliente.Cpf);
        return (true, "Seja Bem-Vindo(a) " + cliente.Nome);
    }

    private (bool valido, string cpfPadronizado) ValidaCpf(string cpf)
    {
        var cpfNormalizado = NormalizarCpf(cpf);

        if (cpfNormalizado.Length != 11)
        {
            return (false, "CPF invalido");
        }

        var cpfPadronizado = PadronizarCpf(cpfNormalizado);
        return (true, cpfPadronizado);
    }

    // Converte o CPF para uma string, adicionando pontos e traços
    private string PadronizarCpf(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }

    // Converte o CPF para um formato numérico, removendo pontos e traços
    private string NormalizarCpf(string cpf)
    {
        return new string(cpf.Where(char.IsDigit).ToArray());
    }
}
