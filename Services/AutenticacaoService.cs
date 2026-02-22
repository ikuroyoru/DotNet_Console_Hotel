using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace DotNet_Console_Hotel.Services;

internal class AutenticacaoService
{
    private readonly SessaoService _sessaoService;
    private readonly ClienteRepositorio _clienteRepositorio;

    public AutenticacaoService(SessaoService sessaoService, ClienteRepositorio clienteRepositorio)
    {
        _sessaoService = sessaoService;
        _clienteRepositorio = clienteRepositorio;
    }

    public Result Cadastrar(string nome, string email, string senha)
    {
        if (_clienteRepositorio.VerificaClienteComEmail(email))
            return Result.Fail($"Erro: O E-Mail {email} inserido ja esta cadastrado.");

        _clienteRepositorio.NovoCliente(nome, email, senha);
        return Result.Ok();
    }

    public Result Login(string email, string senha)
    {
        var cliente = _clienteRepositorio.BuscarClienteAutenticacao(email, senha);

        if (cliente == null)
            return Result.Fail("\"Erro: O E-Mail ou senha estao incorretos. Tente novamente.\"");

        var resultado = _sessaoService.IniciarSessao(cliente.Id);

        if (!resultado.Success)
        {
            return Result.Fail("Erro: Nao foi possivel iniciar a sessao. Tente novamente.");
        }
        return Result.Ok();
    }

    public static Result ValidaFormatoEmail(string email)
    {
        // FUNCAO DEVE VERIFICAR SE
        // - SE O FORMATO GENERICO DO EMAIL ESTA CORRETO

        if (string.IsNullOrWhiteSpace(email))
            return Result.Fail("O email nao pode ser vazio ou conter apenas espacos em branco");

        if(!Regex.IsMatch(
            email,
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        ))
            return Result.Fail("O email inserido nao possui formato valido");
        
        return Result.Ok();
    }
    public static Result ValidaSenha(string senha)
    {
        // A FUNCAO VERIFICA SE
        // - A SENHA POSSUI UM MINIMO DE 8 CARACTERES
        // - A SENHA NAO E VAZIA

        if (string.IsNullOrWhiteSpace(senha))
            return Result.Fail("A senha nao pode ser vazia ou conter apenas espacos em branco");

        if (senha.Length < 8)
            return Result.Fail("A senha deve possuir 8 caracteres");

        return Result.Ok();
    }
    public static Result ValidaNome(string nome)
    {
        // A FUNCAO VERIFICA SE
        // - O NOME NAO E VAZIO
        // - UM NOME TEM PELO MENOS 4 CARACTERES
        // - UM NOME TEM NO MAXIMO 50 CARACTERES

        if(string.IsNullOrWhiteSpace(nome))
            return Result.Fail("O nome nao pode ser vazio ou conter apenas espacos em branco");
        if (nome.Length < 4)
           return Result.Fail("O nome deve possuir no minimo 4 caracteres");
        if(nome.Length > 50)
            return Result.Fail("O nome deve possuir no maximo 50 caracteres");

        return Result.Ok();
    }
    public static Result ValidaConfirmaSenha(string senha, string confirmaSenha)
    {
        // A FUNCAO VERIFICA SE
        // - A CONFIRMACAO DA SENHA EH IGUAL A SENHA INSERIDA

        if (senha != confirmaSenha)
            return Result.Fail("As senhas estao distintas, tente novamente");

        return Result.Ok();
    }
    private bool EmailExiste(string email)
    {
        // FUNCAO DEVE VERIFICAR SE
        // - EMAIL INSERIDO EXISTE ( Enviando email de confirmacao para o email inserido )

        return false;
    }

    /*
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
    */
}
