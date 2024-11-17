using System.Text.Json;
using Challenge7Days.Models;
using Challenge7Days.Models.Common;

namespace Challenge7Days.Services
{
    public class PokemonService
    {
        const string API_URL = "https://pokeapi.co/api/v2/pokemon/";
        private readonly HttpService _httpService;
        public PokemonService()
        {
            _httpService = new HttpService(API_URL);
        }

        public async Task<List<GenericInfo>> GetAllPokemonsSimplified()
        {

            string responseContent = _httpService.Get();

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            PokemonResponse? pokemonsResponse = JsonSerializer.Deserialize<PokemonResponse>(responseContent, options);

            if (pokemonsResponse == null)
                throw new Exception("Was not possible deserialize pokemon response");

            List<GenericInfo> pokemonsSimplified = new List<GenericInfo>(pokemonsResponse.Results);

            if (pokemonsResponse.Next != null)
            {
                string nextUrl = pokemonsResponse.Next;

                while (nextUrl != null)
                {
                    string response = await _httpService.GetAsync(nextUrl);

                    PokemonResponse? pokemonsLoop = JsonSerializer.Deserialize<PokemonResponse>(response, options);

                    if (pokemonsLoop == null)
                        throw new Exception("It was not possible deserialize Pokemon Response");

                    pokemonsSimplified.AddRange(pokemonsLoop.Results);

                    nextUrl = pokemonsLoop.Next;
                }
            }

            return pokemonsSimplified;
        }

        public List<GenericInfo> PokemonGiveAway(List<GenericInfo> pokemons, int amount)
        {
            Random random = new Random();

            amount = Math.Min(amount, pokemons.Count);

            List<GenericInfo> drawnPokemons = new List<GenericInfo>();
            for (int i = 0; i < amount; i++)
            {
                GenericInfo drawnPokemon = pokemons[random.Next(pokemons.Count)];

                if (!drawnPokemons.Any(p => p.Name == drawnPokemon.Name))
                    drawnPokemons.Add(drawnPokemon);
            }

            return drawnPokemons;
        }

        public List<Pokemon> GetPokemonsBySimplified(List<GenericInfo> pokemonsSimplified)
        {
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (var pokemonSimplified in pokemonsSimplified)
            {
                string response = _httpService.Get(pokemonSimplified.Url);

                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNameCaseInsensitive = true
                };

                Pokemon? pokemonDeserialized = JsonSerializer.Deserialize<Pokemon>(response, options);

                if (pokemonDeserialized == null)
                    throw new Exception("It was not possible deserialize Pokemon Information Response");

                pokemons.Add(pokemonDeserialized);
            }

            return pokemons;
        }

    }
}