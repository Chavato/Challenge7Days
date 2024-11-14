using System.Text.Json;
using Challenge7Days.Models;
using Challenge7Days.Models.Common;
using Challenge7Days.Services;

const string API_URL = "https://pokeapi.co/api/v2/pokemon/";

try
{
    Console.WriteLine("Starting Application");

    HttpService httpService = new HttpService(API_URL);

    string responseContent = httpService.Get();

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
            string response = httpService.Get(nextUrl);

            PokemonResponse? pokemonsLoop = JsonSerializer.Deserialize<PokemonResponse>(response, options);

            if (pokemonsLoop == null)
                throw new Exception("It was not possible deserialize Pokemon Response");

            pokemonsSimplified.AddRange(pokemonsLoop.Results);

            nextUrl = pokemonsLoop.Next;
        }
    }

    Console.WriteLine("Starting search about random pokemon details.");
    IList<GenericInfo> pokemonToChooseSimplified = new List<GenericInfo>();

    Random random = new Random();

    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);
    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);
    pokemonToChooseSimplified.Add(pokemonsSimplified[random.Next(pokemonsSimplified.Count)]);

    IList<Pokemon> pokemons = new List<Pokemon>();

    for (int i = 0; i < pokemonToChooseSimplified.Count; i++)
    {
        string response = httpService.Get(pokemonToChooseSimplified[i].Url);

        Pokemon? pokemonDeserialized = JsonSerializer.Deserialize<Pokemon>(response, options);

        if (pokemonDeserialized == null)
            throw new Exception("It was not possible deserialize Pokemon Information Response");

        pokemons.Add(pokemonDeserialized);
        Console.WriteLine();
        Console.WriteLine($"{i + 1} - {pokemonToChooseSimplified[i].Name}:\r\n{pokemonDeserialized}");
    }

    bool repeat = false;
    int pokemonNumber;
    do
    {
        Console.WriteLine();
        Console.WriteLine("With Pokemon do you choose? Digit its number.");
        string? pokemonString = Console.ReadLine();
        repeat = false;

        if (!int.TryParse(pokemonString, out pokemonNumber) || pokemonNumber < 1 || pokemonNumber > 3)
        {
            Console.WriteLine("You can only digit 1, 2, or 3. Try it again.");
            repeat = true;
        }
    }
    while (repeat);

    Console.WriteLine($"You choose {pokemonToChooseSimplified[pokemonNumber - 1].Name}, congratulations!");

}

catch (Exception err)
{
    Console.WriteLine(err.Message);
    if (err.InnerException != null)
        Console.WriteLine(err.InnerException.Message);
}

