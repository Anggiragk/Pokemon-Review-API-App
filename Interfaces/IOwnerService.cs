using PokemonApp.DTO;

namespace PokemonApp.Interfaces
{
    public interface IOwnerService
    {
        public List<OwnerResponse> GetAllOwners();
        public OwnerResponse GetOwnerById(int ownerId);
        public List<OwnerResponse> GetOwnersByPokemonId(int pokemonId);
        public List<PokemonResponse> GetPokemonsByOwnerId(int ownerId);
        public CommonResponse<OwnerResponse> CreateOwner(OwnerRequest ownerRequest);
    }
}
