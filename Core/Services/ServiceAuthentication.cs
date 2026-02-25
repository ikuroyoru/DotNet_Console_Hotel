using System.Text.RegularExpressions;
using DotNet_Console_Hotel.Core.Common;
using DotNet_Console_Hotel.Infrastructure.Repositories;

namespace DotNet_Console_Hotel.Services;

internal class ServiceAuthentication
{
    private readonly ServiceSession _serviceSession;
    private readonly RepositoryClient _repositoryClient;

    public ServiceAuthentication(ServiceSession serviceSession, RepositoryClient repositoryClient)
    {
        _serviceSession = serviceSession;
        _repositoryClient = repositoryClient;
    }

    public Result Register(string name, string email, string password)
    {
        if (_repositoryClient.VerifyClientWithEmail(email))
            return Result.Fail($"Error: This email is already used.");

        _repositoryClient.NewClient(name, email, password);
        return Result.Ok();
    }

    public Result Login(string email, string password)
    {
        var client = _repositoryClient.GetClientByAuthentication(email, password);

        if (client == null)
            return Result.Fail("\"Error: E-Mail or password may be incorrect\"");

        var result = _serviceSession.SessionStart(client.Id);

        if (!result.Success)
        {
            return result;
        }
        return Result.Ok();
    }

    public static Result ValidateEmailFormat(string email)
    {
        // - E-Mail is not empty or whitespace
        // - E-Mail has a valid format (using regular expression)

        if (string.IsNullOrWhiteSpace(email))
            return Result.Fail("The E-Mail cannot be empty or have empty spaces.");

        if(!Regex.IsMatch(
            email,
            @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        ))
            return Result.Fail("Error: Invalid E-Mail format");
        
        return Result.Ok();
    }
    public static Result ValidatePassword(string password)
    {
        // - Password has at least 8 characters
        // - Password is not empty or whitespace

        if (string.IsNullOrWhiteSpace(password))
            return Result.Fail("The password cannot be empty or have empty spaces");

        if (password.Length < 8)
            return Result.Fail("The password must have atleast 8 characters");

        return Result.Ok();
    }
    public static Result ValidadeName(string nome)
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
    public static Result ValidateConfirmPassword(string senha, string confirmaSenha)
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
