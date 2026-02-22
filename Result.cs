using System;
using System.Collections.Generic;
using System.Text;

namespace DotNet_Console_Hotel;

public class Result
{
    public bool Success { get; }
    public string Error { get; }

    private Result(bool success, string error)
    {
        Success = success;
        Error = error;
    }

    public static Result Ok() => new Result(true, "sucesso");
    public static Result Fail(string error) => new Result(false, error);
}
