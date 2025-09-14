using PokedexPM.Models;
using PokedexPM.ViewModels;

namespace PokedexPM.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        BindingContext = new PokemonViewModel();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as PokemonViewModel;
        viewModel?.FilterPokemons(e.NewTextValue);
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Pokemon selectedPokemon)
        {
            
            ((CollectionView)sender).SelectedItem = null;

            
            await Navigation.PushAsync(new PokemonDetailPage(selectedPokemon));
        }
    }
}