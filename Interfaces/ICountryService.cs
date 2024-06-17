using PokemonApp.DTO;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICountryService
    {
        public List<CountryResponse> GetAllCountries();
        public CountryResponse GetCountry(int countryId);
        public CountryResponse GetCountryByOwnerId(int OwnerId);
        public List<OwnerResponse> GetOwnersByCountryId(int countryId);
        public CommonResponse<CountryResponse> CreateCountry(CountryRequest request);

    }
}
