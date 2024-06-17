using PokemonApp.DTO;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IOwnerRepository
    {
        public List<Owner> GetAllOwners();
        public Owner? GetOwner(int ownerId);
        public Owner GetOwnerByFirstname(string firstName);
        public Owner GetOwnerByLastname(string LastName);
        public List<Owner> GetOwnersByPokemonId(int pokemonId);
        public List<Pokemon> GetPokemonsByOwnerId(int ownerId);
        public bool IsOwnerExist(int ownerId);
        public bool CreateOwner(OwnerRequest ownerRequest);
        bool Save();

    }
}
