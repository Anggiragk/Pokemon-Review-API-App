using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Services;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IOwnerService _ownerService;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ICountryRepository _countryRepository;

        public OwnerController(IOwnerRepository ownerRepository, IOwnerService ownerService, IPokemonRepository pokemonRepository, ICountryRepository countryRepository)
        {
            _ownerRepository = ownerRepository;
            _ownerService = ownerService;
            _pokemonRepository = pokemonRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public ActionResult GetAllOwner()
        {
            List<OwnerResponse> ownerResponses = _ownerService.GetAllOwners();
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(ownerResponses);
        }

        [HttpGet("{OwnerId}")]
        public ActionResult GetOwnerById(int ownerId)
        {
            if(!_ownerRepository.IsOwnerExist(ownerId))
                return NotFound();

            OwnerResponse ownerResponse = _ownerService.GetOwnerById(ownerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(ownerResponse);
        }

        [HttpGet("{OwnerId}/pokemon")]
        public ActionResult GetPokemonsByOwnerId(int ownerId)
        {
            if (!_ownerRepository.IsOwnerExist(ownerId))
                return NotFound($"Owner with id{ownerId} not found");

            List<PokemonResponse> responses = _ownerService.GetPokemonsByOwnerId(ownerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(responses);
        }

        [HttpGet("pokemon/{pokemonId}")]
        public IActionResult GetOwnesrByPokemonId(int pokemonId)
        {
            if(!_pokemonRepository.IsPokemonExist(pokemonId))
                return NotFound($"Pokemon with id {pokemonId} Not Found");

            List<OwnerResponse> ownerResponses = _ownerService.GetOwnersByPokemonId(pokemonId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(ownerResponses);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] OwnerRequest request)
        {
            if (!_countryRepository.IsCountryExist(request.CountryId))
                return NotFound("Country not found");

            if (request == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _ownerService.CreateOwner(request);

            if (response.Status != 201)
                return BadRequest(response);

            return Ok(response);
        }
    }
}
