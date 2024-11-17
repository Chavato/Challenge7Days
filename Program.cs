using Challenge7Days.Models;
using Challenge7Days.Models.Common;
using Challenge7Days.Services;

try
{
    PokemonService pokemonService = new PokemonService();

    UserInteractionService userInteractionService = new UserInteractionService();

    Task<List<GenericInfo>> allPokemonsSimplifiedTask = pokemonService.GetAllPokemonsSimplified();

    userInteractionService.WelcomeScript();

    int pokemonAmount = userInteractionService.GetPokemonOptionsNumber();

    List<GenericInfo> allPokemonsSimplified = await allPokemonsSimplifiedTask;

    List<GenericInfo> drawnPokemonsSimplified = pokemonService.PokemonGiveAway(allPokemonsSimplified, pokemonAmount);

    List<Pokemon> pokemonsOptions = pokemonService.GetPokemonsBySimplified(drawnPokemonsSimplified);

    Pokemon? choosenPokemon = userInteractionService.ChoosePokemon(pokemonsOptions);

    List<Pokemon> pokemons = new List<Pokemon>();

    if (choosenPokemon != null)
        pokemons.Add(choosenPokemon);

    userInteractionService.MenuMain(pokemonsOptions, pokemons);

}

catch (Exception err)
{
    Console.WriteLine(err.Message);
    if (err.InnerException != null)
        Console.WriteLine(err.InnerException.Message);
}

