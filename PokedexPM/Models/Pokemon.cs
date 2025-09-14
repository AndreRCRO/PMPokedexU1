using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokedexPM.Models
{
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("sprites")]
        public Sprite Sprites { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonTypeSlot> Types { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Sprite
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }
    }

    public class PokemonTypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public TypeInfo Type { get; set; }
    }

    public class TypeInfo
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public int Url { get; set; }
    }
}
