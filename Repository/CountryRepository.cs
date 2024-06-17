using Azure.Core;
using PokemonApp.Data;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateCountry(CountryRequest countryRequest)
        {
            Country country= new Country
            {
                Name = countryRequest.Name
            };

            _context.Add(country);

            return Save();
        }

        public List<Country> GetAllCountries()
        {
            return _context.Countries.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByName(string name)
        {
            return _context.Countries.Where(c => c.Name == name).FirstOrDefault();
        }

        public Country GetCountryByOwnerId(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();
        }

        public List<Owner> GetOwnersByCountryId(int countryId)
        {
            return _context.Owners.Where(o => o.Country.Id == countryId).ToList();
        }

        public bool IsCountryExist(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
