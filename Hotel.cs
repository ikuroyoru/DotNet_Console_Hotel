using System;

public class Hotel
{
	public Hotel(string nome, List<int> quartos)
	{
		Nome = nome;
		Quartos = quartos;
	}

	public string Nome { get; set; }
	public List<int> Quartos { get; set; } // TRANSFORMA 'QUARTOS' EM UM OBJETO COM (NUMERO, STATUSRESERVA, OBJETO DA RESERVA)



	// CRIAR FUNCAO DE RESERVA DE QUARTOS

}



