
using PokedexPM.Models;
using PokedexPM.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokedexPM.ViewModels
{
    internal class PokemonViewModel : INotifyPropertyChanged
    {
        private List<Pokemon> _allPokemons = new List<Pokemon>();  
        public List<Pokemon> FilteredPokemons { get; set; }
        public PokeApiService PokeApiService = new PokeApiService(new HttpClient());

        public PokemonViewModel()
        {
            LoadAllPokemons();
        }


        public async Task LoadAllPokemons()
        {
            _allPokemons = await PokeApiService.GetAllPokemons();
            FilteredPokemons = new List<Pokemon>(_allPokemons);
            OnPropertyChanged(nameof(FilteredPokemons));
        }

        public void FilterPokemons(string search)
        {
            if (string.IsNullOrEmpty(search))
                FilteredPokemons = new List<Pokemon>(_allPokemons);
            else 
                FilteredPokemons = _allPokemons
                    .Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            OnPropertyChanged(nameof(FilteredPokemons));
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
