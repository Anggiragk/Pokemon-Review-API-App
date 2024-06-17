using PokemonApp.DTO;

namespace PokemonApp.Interfaces
{
    public interface IPokemonService
    {
        public List<PokemonResponse> GetPokemonList();
        public PokemonResponse GetPokemon(int id);
        public CommonResponse<PokemonResponse> createPokemon(PokemonRequest request);
    }
}
