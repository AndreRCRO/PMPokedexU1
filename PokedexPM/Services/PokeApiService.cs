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
            string Url = "https://pokeapi.co/api/v2/pokemon?limit=100000&offset=0";
            var response = await _client.GetFromJsonAsync<PokemonListResponse>(Url);

            return response?.Results?.Select(item => new Pokemon
            {
                Name = item.Name,
                Url = item.Url
            }).ToList() ?? new List<Pokemon>();
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
