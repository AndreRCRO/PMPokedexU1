using PokedexPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PokedexPM.Services
{
    internal class PokeApiService
    {
        private readonly HttpClient _client;
        private List<Pokemon> _pokemons;

        public PokeApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Pokemon> GetPokemonDetailsByName(string name)
        {
            string baseUrl = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}";
            return await _client.GetFromJsonAsync<Pokemon>(baseUrl);
        }

        public async Task<List<Pokemon>> GetAllPokemons()
        {
            string Url = "https://pokeapi.co/api/v2/pokemon?limit=10&offset=0";
            var response = await _client.GetFromJsonAsync<PokemonListResponse>(Url);

            var results = response?.Results ?? new List<PokemonListItem>();
            var pokemons = new List<Pokemon>();

            foreach (var item in results)
            {
                var details = await GetPokemonDetailsByName(item.Name);
                if (details != null)
                    pokemons.Add(details);
            }

            return pokemons;
        }
    }

    public class PokemonListResponse
    {
        public List<PokemonListItem> Results { get; set; }
    }

    public class PokemonListItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
