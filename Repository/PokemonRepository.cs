using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;

        public PokemonRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreatePokemon(Pokemon pokemon, Owner owner, Category Category)
        {
            var pokemonOwner = new PokemonOwner
            {
                Owner = owner,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory
            {
                Category = Category,
                Pokemon = pokemon
            };

            _context.Add(pokemonCategory);
            _context.Add(pokemon);
            return Save();
        }

        public List<Category> GetCategoriesByPokemon(int pokemonId)
        {
            return _context.PokemonCategories.Where(p => p.PokemonId == pokemonId).Select(p => p.Category).ToList();
        }

        public Pokemon GetPokemon(int pokemonId)
        {
            return _context.Pokemons.Where(p => p.Id == pokemonId).FirstOrDefault();
        }

        public Pokemon GetPokemon(string pokemonName)
        {
            return _context.Pokemons.Where(p => p.Name == pokemonName).FirstOrDefault();
        }

        public List<Pokemon> GetPokemons()
        {
            List<Pokemon> pokemons = _context.Pokemons.OrderBy(p => p.Id).ToList();
            return pokemons;
        }

        public bool IsPokemonExist(int pokemonId)
        {
            return _context.Pokemons.Any(p => p.Id == pokemonId);
        }

        public decimal PokemonRating(int pokemonId)
        {
            var review = _context.Reviews.Where(r => r.Pokemon.Id == pokemonId);
            if (review.Count() <=0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
