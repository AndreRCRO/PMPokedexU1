using PokedexPM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PokedexPM.ViewModels
{
    internal class PokemonDetailViewModel : INotifyPropertyChanged
    {
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public int PokedexNumber { get; set; }
        public List<string> Types { get; set; } = new List<string>();

        public PokemonDetailViewModel(Pokemon pokemon)
        {
            if (pokemon != null)
            {
                DisplayName = pokemon.Name;
                ImageUrl = pokemon.Sprites?.FrontDefault;
                PokedexNumber = pokemon.Id;
                Types = pokemon.Types?.ConvertAll(t => t.Type.Name) ?? new List<string>();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
