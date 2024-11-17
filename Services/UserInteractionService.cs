using System.Globalization;
using Challenge7Days.Models;

namespace Challenge7Days.Services
{
    public class UserInteractionService
    {
        public User user { get; private set; }
        
        public UserInteractionService()
        {
            user = new User();
        }

        public void WelcomeScript()
        {
            user.Name = GetUserName();
            Console.WriteLine($"Lets play {user.Name}!!");
        }

        private string GetUserName()
        {
            Console.WriteLine("What's your name?");

            string name = ReadString();

            return name;
        }

        public int GetPokemonOptionsNumber()
        {
            Console.WriteLine("How many pokemons options do you want to choose?");

            int pokemonAmount = ReadInt();

            return pokemonAmount;
        }

        public Pokemon? ChoosePokemon(List<Pokemon> pokemonOptions)
        {

            if (pokemonOptions.Count == 0)
            {
                Console.WriteLine("There's no pokemon to choose.");
                return null;
            }

            bool loopCheck;
            int index;
            do
            {
                loopCheck = false;
                Console.WriteLine("Choose one of those pokemons:");

                for (int i = 0; i < pokemonOptions.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {pokemonOptions[i]}");
                }

                index = ReadInt();

                if (index < 1 || index > pokemonOptions.Count)
                {
                    Console.WriteLine($"Please, digit a number between 1 and {pokemonOptions.Count}.");
                    loopCheck = true;
                }
            }

            while (loopCheck);

            Pokemon choosenPokemon = pokemonOptions.ElementAt(index - 1);
            pokemonOptions.RemoveAt(index - 1);

            Console.WriteLine($"Congrats! You choose: {choosenPokemon}");

            return choosenPokemon;
        }

        public void MenuMain(List<Pokemon> pokemonOptions, List<Pokemon> chosenPokemons)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("----------------MENU----------------");
                Console.WriteLine("What do you want?");
                Console.WriteLine("1 - Adopt a pokemon.");
                Console.WriteLine("2 - See adopted pokemons.");
                Console.WriteLine("3 - Exit.");

                int numberMenu = ReadInt();

                switch (numberMenu)
                {
                    case 1:
                        Pokemon? pokemonChoosen = ChoosePokemon(pokemonOptions);

                        if (pokemonChoosen != null)
                            chosenPokemons.Add(pokemonChoosen);
                        break;

                    case 2:
                        foreach (Pokemon pokemon in chosenPokemons)
                        {
                            Console.WriteLine($"{pokemon}");
                        }
                        break;

                    case 3:
                        Console.WriteLine("Thanks for playing!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Please, digit a valid number.");
                        break;
                }
            }
            while (!exit);
        }

        private int ReadInt()
        {
            bool loopCheck;
            int number;
            do
            {
                loopCheck = false;
                string? readText = Console.ReadLine();

                if (!int.TryParse(readText, out number))
                {
                    Console.WriteLine("Please, digit a number.");
                    loopCheck = true;
                }
            }
            while (loopCheck);

            return number;
        }

        private string ReadString()
        {
            bool loopCheck;
            string? text = string.Empty;
            do
            {
                loopCheck = false;
                text = Console.ReadLine();

                if (text == null)
                {
                    Console.WriteLine("The input is invalid, try again.");
                    loopCheck = true;
                }
            }
            while (loopCheck);

            return text;
        }

    }
}