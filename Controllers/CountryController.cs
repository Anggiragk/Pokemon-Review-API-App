using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountryService _countryService;

        public CountryController(ICountryRepository countryRepository, ICountryService countryService)
        {
            _countryRepository = countryRepository;
            _countryService = countryService;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            List<CountryResponse> countryResponse = _countryService.GetAllCountries();
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(countryResponse);
        }

        [HttpGet("{countryId}")]
        public IActionResult GetCountry(int countryId)
        {
            if(!_countryRepository.IsCountryExist(countryId))
                return NotFound();

            var country = _countryService.GetCountry(countryId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpGet("owners/{OwnerId}")]
        public IActionResult GetCountryByOwnerId(int ownerId)
        {
            CountryResponse country = _countryService.GetCountryByOwnerId(ownerId);
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] CountryRequest request)
        {
            if(request == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _countryService.CreateCountry(request);

            if (response.Status != 201)
                return BadRequest(response);

            return Ok(response);
        }

    }
}
