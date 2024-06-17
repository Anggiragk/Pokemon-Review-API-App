using PokemonApp.DTO;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICountryRepository
    {
        public List<Country> GetAllCountries();
        public Country GetCountry(int id);
        public Country GetCountryByName(string name);
        public Country GetCountryByOwnerId(int ownerId);
        public List<Owner> GetOwnersByCountryId(int countryId);
        public bool IsCountryExist(int countryId);
        public bool CreateCountry (CountryRequest countryRequest);
        public bool Save();
    }
}
