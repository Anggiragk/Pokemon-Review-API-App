using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IPokemonRepository
    {
        public List<Pokemon> GetPokemons();
        public bool IsPokemonExist(int pokemonId);
        public Pokemon GetPokemon(int pokemonId);
        public Pokemon GetPokemon(string pokemonName);
        public decimal PokemonRating(int pokemonId);
        public List<Category> GetCategoriesByPokemon(int pokemonId);
        public bool CreatePokemon(Pokemon pokemon, Owner owner, Category category);
        public bool Save();
    }
}
