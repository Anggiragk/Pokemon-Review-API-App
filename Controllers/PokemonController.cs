using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Repository;
using PokemonApp.Services;
using ReviewApp.Models;

namespace PokemonApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IPokemonService _pokemonService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOwnerRepository _ownerRepository;

        public PokemonController(IPokemonRepository pokemonRepository, IPokemonService pokemonService, ICategoryRepository category, IOwnerRepository owner)
        {
            _pokemonRepository = pokemonRepository;
            _pokemonService = pokemonService;
            _categoryRepository = category;
            _ownerRepository = owner;
        }

        [HttpGet(Name = "Get All Pokemons")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonResponse>))]
        public IActionResult GetPokemons()
        {
            List<PokemonResponse> pokemons = _pokemonService.GetPokemonList();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons);
        }

        [HttpGet("{pokemonId}")]
        [ProducesResponseType(200, Type = typeof(PokemonResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPokemon(int pokemonId)
        {
            if (!_pokemonRepository.IsPokemonExist(pokemonId))
                return NotFound($"Pokemon with Id {pokemonId} not found");

            PokemonResponse pokemon = _pokemonService.GetPokemon(pokemonId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemon);
        }

        [HttpGet("{pokemonId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult getRating(int pokemonId)
        {
            if (!_pokemonRepository.IsPokemonExist(pokemonId))
                return NotFound();

            decimal rating = _pokemonRepository.PokemonRating(pokemonId);
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CommonResponse<PokemonResponse>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromBody] PokemonRequest request)
        {
            if (request == null)
                return BadRequest();

            if (!_ownerRepository.IsOwnerExist(request.OwnerId))
                return NotFound("owner not found");
            
            if(!_categoryRepository.IsCategoryExist(request.CategoryId))
                return NotFound("category not found");

            var response = _pokemonService.createPokemon(request);

            if (!ModelState.IsValid)
                return BadRequest();
            
            return StatusCode(response.Status, response);
        }
    }
}
