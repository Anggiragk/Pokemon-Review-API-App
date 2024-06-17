using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;
using ReviewApp.Models;

namespace PokemonApp.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public OwnerService (IOwnerRepository ownerRepository, IPokemonRepository pokemonRepository)
        {
            _ownerRepository = ownerRepository;
            _pokemonRepository = pokemonRepository;
        }

        public CommonResponse<OwnerResponse> CreateOwner(OwnerRequest ownerRequest)
        {
            if (
                _ownerRepository.GetOwnerByFirstname(ownerRequest.FirstName) != null
                &&
                _ownerRepository.GetOwnerByLastname(ownerRequest.LastName) != null
                )
            {
                return new CommonResponse<OwnerResponse>
                {
                    Status = 400,
                    Message = "failed to save, owner already exist",
                    Errors = "true"
                };
            }

            var save = _ownerRepository.CreateOwner(ownerRequest);
            if (save)
            {
                var owner = _ownerRepository.GetOwnerByFirstname(ownerRequest.FirstName);
                return new CommonResponse<OwnerResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = Transform.ToOwnerResponse(owner)
                };
            }
            else
            {
                return new CommonResponse<OwnerResponse>
                {
                    Status = 400,
                    Message = "Failed",
                    Errors = "true"
                };
            }
        }

        public List<OwnerResponse> GetAllOwners()
        {
            List<Owner> owners = _ownerRepository.GetAllOwners();
            List<OwnerResponse> ownerResponses = new List<OwnerResponse>();
            owners.ForEach(o =>
            {
                ownerResponses.Add(Transform.ToOwnerResponse(o));
            });
            return ownerResponses;
        }

        public OwnerResponse GetOwnerById(int ownerId)
        {
            Owner? owner = _ownerRepository.GetOwner(ownerId);

            if (owner == null)
            {
                throw new Exception("owner not found");
            }

            Console.WriteLine(owner.Country.Id);
            Console.WriteLine(owner.Country.Name);
            return Transform.ToOwnerResponse(owner);
        }

        public List<OwnerResponse> GetOwnersByPokemonId(int pokemonId)
        {
            List<Owner> owners = _ownerRepository.GetOwnersByPokemonId(pokemonId);
            List<OwnerResponse> ownerResponses = new List<OwnerResponse>();
            owners.ForEach(o =>
            {
                ownerResponses.Add(Transform.ToOwnerResponse(o));
            });
            return ownerResponses;
        }

        public List<PokemonResponse> GetPokemonsByOwnerId(int ownerId)
        {
            List<Pokemon> pokemons = _ownerRepository.GetPokemonsByOwnerId(ownerId);
            List<PokemonResponse> pokemonResponses = new List<PokemonResponse>();

            pokemons.ForEach(p =>
            {
                var categories = _pokemonRepository.GetCategoriesByPokemon(p.Id);
                pokemonResponses.Add(Transform.ToPokemonResponse(p, categories));
            });
            return pokemonResponses;
        }
    }
}
