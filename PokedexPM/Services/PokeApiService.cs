using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PokedexPM.Models;

namespace PokedexPM.Services
{
    internal class PokeApiService
    {
        private readonly HttpClient _client;

        public PokeApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Pokemon?> GetPokemonDetailsByName(string name, CancellationToken ct = default)
        {
            string baseUrl = $"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}";
            return await _client.GetFromJsonAsync<Pokemon>(baseUrl, ct);
        }

        public async Task<List<Pokemon>> GetAllPokemons(
            int limit = 10,
            int offset = 0,
            int degreeOfParallelism = 6,
            CancellationToken ct = default)
        {
            string url = $"https://pokeapi.co/api/v2/pokemon?limit={limit}&offset={offset}";
            var response = await _client.GetFromJsonAsync<PokemonListResponse>(url, ct);
            var results = response?.Results ?? new List<PokemonListItem>();

            //Executor :D
            var bag = new ConcurrentBag<Pokemon>();

            await Parallel.ForEachAsync(
                results,
                new ParallelOptions
                {
                    MaxDegreeOfParallelism = degreeOfParallelism,
                    CancellationToken = ct
                },
                async (item, token) =>
                {
                    try
                    {
                        var pokemon = await GetPokemonDetailsByName(item.Name, token);
                        if (pokemon != null)
                            bag.Add(pokemon);
                    }
                    catch
                    { }
                });

            return bag.ToList();
        }
    }

    public class PokemonListResponse
    {
        public List<PokemonListItem> Results { get; set; } = new();
    }

    public class PokemonListItem
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
    }
}
