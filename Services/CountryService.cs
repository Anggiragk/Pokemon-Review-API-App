using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Repository;

namespace PokemonApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public CommonResponse<CountryResponse> CreateCountry(CountryRequest request)
        {
            if (_countryRepository.GetCountryByName(request.Name) != null)
            {
                return new CommonResponse<CountryResponse>
                {
                    Status = 400,
                    Message = "failed to save, country already exist",
                    Errors = "true"
                };
            }

            var save = _countryRepository.CreateCountry(request);
            if (save)
            {
                var country = _countryRepository.GetCountryByName(request.Name);
                return new CommonResponse<CountryResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = Transform.ToCountryResponse(country)
                };
            }
            else
            {
                return new CommonResponse<CountryResponse>
                {
                    Status = 400,
                    Message = "Failed",
                    Errors = "true"
                };
            }
        }

        public List<CountryResponse> GetAllCountries()
        {
            List<Country> countries = _countryRepository.GetAllCountries();
            List<CountryResponse> countryResponses = new List<CountryResponse>();

            countries.ForEach(c =>
            {
                countryResponses.Add(Transform.ToCountryResponse(c));
            });
            return countryResponses;
        }

        public CountryResponse GetCountry(int countryId)
        {
            Country country = _countryRepository.GetCountry(countryId);
            return Transform.ToCountryResponse(country);
        }

        public CountryResponse GetCountryByOwnerId(int OwnerId)
        {
            Country country = _countryRepository.GetCountryByOwnerId(OwnerId);
            return Transform.ToCountryResponse(country);
        }

        public List<OwnerResponse> GetOwnersByCountryId(int countryId)
        {
            List<Owner> owners = _countryRepository.GetOwnersByCountryId(countryId);
            List<OwnerResponse> ownerResponses = new List<OwnerResponse>();

            owners.ForEach(o =>
            {
                ownerResponses.Add(Transform.ToOwnerResponse(o));
            });
            return ownerResponses;
        }
    }
}
