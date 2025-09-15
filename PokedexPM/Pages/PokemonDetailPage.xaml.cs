using PokedexPM.Models;
using PokedexPM.ViewModels;

namespace PokedexPM.Pages;

public partial class PokemonDetailPage : ContentPage
{

	public PokemonDetailPage(Pokemon selectedPokemon)
	{
		InitializeComponent();
        BindingContext = new PokemonDetailViewModel(selectedPokemon);
    }
}