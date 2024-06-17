using PokemonApp.DTO;
using PokemonApp.Interfaces;
using ReviewApp.Models;

namespace PokemonApp.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOwnerRepository _ownerRepository;

        public PokemonService(IPokemonRepository pokemonRepository, ICategoryRepository categoryRepository, IOwnerRepository ownerRepository)
        {
            _pokemonRepository = pokemonRepository;
            _categoryRepository = categoryRepository;
            _ownerRepository = ownerRepository;
        }

        public List<PokemonResponse> GetPokemonList()
        {
            var pokemons = _pokemonRepository.GetPokemons();

            List<PokemonResponse> pokemonResponses = new List<PokemonResponse>();

            pokemons.ForEach(p =>
            {
                var categories = _pokemonRepository.GetCategoriesByPokemon(p.Id);

                pokemonResponses.Add(Transform.ToPokemonResponse(p, categories));
            });
            return pokemonResponses;
        }

        public PokemonResponse GetPokemon(int id)
        {
            Pokemon pokemon = _pokemonRepository.GetPokemon(id);
            var categories = _pokemonRepository.GetCategoriesByPokemon(id);
            return Transform.ToPokemonResponse(pokemon, categories);
        }

        public CommonResponse<PokemonResponse> createPokemon(PokemonRequest request)
        {
            var owner = _ownerRepository.GetOwner(request.OwnerId);
            var category = _categoryRepository.GetCategory(request.CategoryId);
            var pokemon = new Pokemon
            {
                Name = request.Name,
                BirthDate = request.BirthDate,
            };

            var save = _pokemonRepository.CreatePokemon(pokemon, owner, category);

            if (save)
            {
                return new CommonResponse<PokemonResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = new PokemonResponse
                    {
                        Id = pokemon.Id,
                        Name = pokemon.Name,
                        BirthDate = pokemon.BirthDate,
                        Categories = new List<CategoryResponse>
                        {
                            new() {Id = category.Id, Name =  category.Name} 
                        }
                    }
                };
            }
            else
            {
                return new CommonResponse<PokemonResponse>
                {
                    Status = 400,
                    Message = "Error",
                    Errors = "error, failed create new Pokemon"
                };
            }
        }
    }
}
