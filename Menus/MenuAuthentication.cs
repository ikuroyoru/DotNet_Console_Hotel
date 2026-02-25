namespace DotNet_Console_Hotel.Menus;

using DotNet_Console_Hotel.Core.Common;
using DotNet_Console_Hotel.Services;

internal class MenuAuthentication : Menu
{
    private readonly ServiceAuthentication _serviceAuthentication;
    private readonly MenuMain _menuMain;
    private readonly List<string> _loginOptions = new()
    {
        "Login",
        "Create an Account",
    };

    public MenuAuthentication(ServiceAuthentication serviceAuthentication, MenuMain manuMain)
    {
        _serviceAuthentication = serviceAuthentication;
        _menuMain = manuMain;
    }
    public override void Execute()
    {
        base.Execute();
        while (true)
        {
            ShowAuthenticationOptions();
            int selection = ValidateSelection();
            ProccessAuthenticationOption(selection);
        }
    }
    private void ShowAuthenticationOptions()
    {
        Console.WriteLine("***** Welcome to HotelBooker.com *****\n");

        for (int i = 0; i < _loginOptions.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {_loginOptions[i]}");
        }
    }
    private int ValidateSelection()
    {
        bool validSelection = false;
        int selection = 0;

        while (!validSelection)
        {
            Console.Write("Select an option");
            string selectionInString = Console.ReadLine()!;

            if (!int.TryParse(selectionInString, out int selectionInInt))
            {
                Console.WriteLine("Invalid entry. Write a number");
                continue;
            }

            if (selectionInInt > _loginOptions.Count || selectionInInt < 1)
            {
                Console.WriteLine($"Invalid selection. Try again");
                Console.WriteLine($"Selected {selectionInInt} of a maximum {_loginOptions.Count}");
            }
            else
            {
                selection = selectionInInt;
                validSelection = true;
                break;
            }
        }
        return selection;
    }
    private void ProccessAuthenticationOption(int selection)
    {
        switch (selection)
        {
            case 1:
                LogimForms();
                break;
            case 2:
                RegisterForms();
                break;
            default:
                Console.WriteLine("Error: Invalid Option");
                break;
        }
    }
    private void RegisterForms()
    {
        Console.WriteLine("**** Create an Account ****");

        string[] fields = {"Full Name", "E-Mail", "Password", "Confirm the Password" };
        string name = string.Empty;
        string email = string.Empty;
        string password = string.Empty;

        int i = 0;

        while (i < fields.Length) // VALIDATE FIELDS BEFORE REGISTER
        {
            Console.Write($"{fields[i]}: ");
            string input = Console.ReadLine()!;

            Result validateRegisterData = i switch
            {
                0 => ServiceAuthentication.ValidadeName(input),
                1 => ServiceAuthentication.ValidateEmailFormat(input),
                2 => ServiceAuthentication.ValidatePassword(input),
                3 => ServiceAuthentication.ValidateConfirmPassword(password, input),
                _ => Result.Fail("Campo inválido")
            };

            if (validateRegisterData.Success)
            {
                switch (i)
                {
                    case 0: name = input; break;
                    case 1: email = input; break;
                    case 2: password = input; break;
                }

                i++;
                Console.Clear();
            }
            else
            {
                Console.WriteLine(validateRegisterData.Error);
                Console.WriteLine("Error: Try again.\n");
            }
        }

        var validateRegisterAction = _serviceAuthentication.Register(name, email, password);
        if (!validateRegisterAction.Success)
        {
            Console.WriteLine(validateRegisterAction.Error);
            Thread.Sleep(4000);
            Execute();
        }
        else
        {
            _serviceAuthentication.Login(email, password);
            _menuMain.Execute();
        }

    }
    private void LogimForms()
    {
        Console.WriteLine("***** LOGIN *****");

        Console.Write("\nE-Mail: ");
        string email = Console.ReadLine()!;

        Console.Write("Senha: ");
        string password = Console.ReadLine()!;

        var validateLoginAction = _serviceAuthentication.Login(email, password);

        if (!validateLoginAction.Success)
        {
            Console.WriteLine(validateLoginAction.Error);
            Console.WriteLine("\nPress any key to get back to main menu");
            Console.ReadKey();
            Execute();
            return;
        }

        Console.WriteLine("Logged in!");
        Thread.Sleep(2500);
        _menuMain.Execute();
    }
}
