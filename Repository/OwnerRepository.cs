using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PokemonApp.Data;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(OwnerRequest ownerRequest)
        {
            Country country = _context.Countries.Where(c => c.Id == ownerRequest.CountryId).FirstOrDefault();
            Owner owner= new Owner
            {
                FirstName = ownerRequest.FirstName,
                LastName = ownerRequest.LastName,
                Country = country
            };
            _context.Add(owner);
            return Save();
        }

        public List<Owner> GetAllOwners()
        {
            return _context.Owners.Include(o => o.Country).OrderBy(o => o.Id).ToList();
        }

        public Owner? GetOwner(int ownerId)
        {
            return _context.Owners
                .Where(o => o.Id == ownerId)
                .Include(o => o.Country)
                .FirstOrDefault();
        }

        public Owner GetOwnerByFirstname(string firstName)
        {
            return _context.Owners.Where(o => o.FirstName == firstName).FirstOrDefault();
        }

        public Owner GetOwnerByLastname(string lastName)
        {
            return _context.Owners.Where(o => o.LastName == lastName).FirstOrDefault();
        }

        public List<Owner> GetOwnersByPokemonId(int pokemonId)
        {
            return _context.PokemonOwners.Where(p => p.PokemonId == pokemonId).Select(p=> p.Owner).ToList();
        }

        public List<Pokemon> GetPokemonsByOwnerId(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.OwnerId == ownerId).Select(o => o.Pokemon).ToList();
        }

        public bool IsOwnerExist(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
