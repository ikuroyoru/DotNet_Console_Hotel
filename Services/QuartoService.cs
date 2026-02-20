using System;
using System.Collections.Generic;
using DotNet_Console_Hotel.Models;
using DotNet_Console_Hotel.Repositorios;

namespace DotNet_Console_Hotel.Services;

internal class QuartoService
{
    private readonly QuartoRepositorio _quartoRepositorio;

    public QuartoService(QuartoRepositorio quartoRepositorio)
    {
        _quartoRepositorio = quartoRepositorio;
    }


}
