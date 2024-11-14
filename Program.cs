using System.Text.Json;
using Challenge7Days.Models;
using RestSharp;


const string API_URL = "https://pokeapi.co/api/v2/pokemon/";

try
{
    Console.WriteLine("Starting Application");
    RestClient client = new RestClient(API_URL);

    RestRequest initialRequest = new RestRequest("", Method.Get);

    Console.WriteLine("Starting search about all pokemons.");
    RestResponse response = client.Get(initialRequest);

    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        throw new Exception("Error");

    if (response.Content == null)
        throw new Exception("response content is missing.");

    JsonSerializerOptions options = new JsonSerializerOptions()
    {
        PropertyNameCaseInsensitive = true
    };

    PokemonResponse? pokemonsResponse = JsonSerializer.Deserialize<PokemonResponse>(response.Content, options);

    if (pokemonsResponse == null)
        throw new Exception("Was not possible deserialize pokemon response");

    List<PokemonSimplified> pokemonsSimplified = new List<PokemonSimplified>(pokemonsResponse.Results);

    // if (pokemonsResponse.Next != null)
    // {
    //     string nextUrl = pokemonsResponse.Next;

    //     while (nextUrl != null)
    //     {
    //         RestRequest requestLoop = new RestRequest(nextUrl, Method.Get);

    //         RestResponse responseLoop = client.Get(requestLoop);

    //         if (responseLoop.Content == null)
    //             throw new ArgumentNullException("Content from response is null.");

    //         PokemonResponse? pokemonsLoop = JsonSerializer.Deserialize<PokemonResponse>(responseLoop.Content, options);

    //         if (pokemonsLoop == null)
    //             throw new Exception("It was not possible deserialize Pokemon Response");

    //         pokemonsSimplified.AddRange(pokemonsLoop.Results);

    //         nextUrl = pokemonsLoop.Next;
    //     }
    // }

    Console.WriteLine("Starting search about random pokemon details.");
    IList<PokemonSimplified> pokemonToChooseSimplified = new List<PokemonSimplified>();

    Random random = new Random();

    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);
    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);
    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);

    IList<Pokemon> pokemons = new List<Pokemon>();

    foreach (var pokemon in pokemonToChooseSimplified)
    {
        RestRequest pokemonInformationRequest = new RestRequest(pokemon.Url, Method.Get);
        RestResponse pokemonInformationResponse = client.Get(pokemonInformationRequest);

        if (pokemonInformationResponse.Content == null)
            throw new ArgumentNullException("Content from response is null.");

        Pokemon? pokemonDeserialized = JsonSerializer.Deserialize<Pokemon>(pokemonInformationResponse.Content, options);

        if (pokemonDeserialized == null)
            throw new Exception("It was not possible deserialize Pokemon Information Response");

        pokemons.Add(pokemonDeserialized);

        System.Console.WriteLine();
        System.Console.WriteLine($"About {pokemon.Name}:\r\n{pokemonDeserialized}");
    }

}

catch (Exception err)
{
    System.Console.WriteLine(err.Message);
    if (err.InnerException != null)
        System.Console.WriteLine(err.InnerException.Message);
}

