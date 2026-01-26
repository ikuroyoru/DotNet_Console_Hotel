using System;

internal class HotelRepositorio
{
	public List<Hotel> Hoteis = new();

	public void Adicionar( Hotel hotel )
	{
	    Hoteis.Add(hotel);
	}
}
