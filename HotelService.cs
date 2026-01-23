using System;
using System.Reflection.Metadata.Ecma335;

public class HotelService
{
	public HotelService(HotelRepositorio _hotelRepositorio)
	{
		hotelRepositorio = _hotelRepositorio;
	}

	HotelRepositorio hotelRepositorio;

	public (bool sucesso, string mensagem) CriarHotel(string nome, string quantidadeDeQuartos)
	{
		if (!(int.TryParse(quantidadeDeQuartos, out int qtdQuartos))) return (false, "Entrada invalida: Quantidade de quartos deve ser um numero inteiro");

		if (qtdQuartos <= 0) return (false, ("Um hotel deve possuir quartos"));

		if (nome.Length <= 0) return (false, ("Um hotel deve possuir um nome"));




		List<int> listaDeQuartos = new List<int>();

		for (int i = 1; i <= qtdQuartos; i++)
		{
			listaDeQuartos.Add(i);
		}

		Hotel hotel = new Hotel(nome, listaDeQuartos);

		hotelRepositorio.Adicionar(hotel);

		return (true, ("Hotel " + hotel.Nome + " adicionado com sucesso"));
	}

	public List<Hotel> ObterHoteis()
	{
		return hotelRepositorio.Hoteis;
	}

	
}
