using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using PokemonApp.Services;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryService _categoryService;

        public CategoryController (ICategoryRepository categoryRepository, ICategoryService categoryService)
        {
            _categoryRepository = categoryRepository;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategory() 
        {
            List<CategoryResponse> categoryResponses = _categoryService.GetAllCategory();
            
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(categoryResponses);
        }

        [HttpGet("{CategoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            if(!_categoryRepository.IsCategoryExist(categoryId))
                return NotFound();

            CategoryResponse categoryResponse = _categoryService.GetCategory(categoryId);
            
            if (!ModelState.IsValid)
                return BadRequest();
            
            return Ok(categoryResponse);
        }

        [HttpGet("pokemon/{CategoryId}")]
        public IActionResult GetPokemonsByCategoryId(int categoryId)
        {
            if (!_categoryRepository.IsCategoryExist(categoryId))
                return NotFound();

            List<PokemonResponse> pokemonResponses = _categoryService.GetPokemonsByCategory(categoryId);
            
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemonResponses);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryRequest request)
        {
            if(request == null)
                return BadRequest(ModelState);

            var response = _categoryService.CreateCategory(request);

            if (response.Status == 400)
                return BadRequest(response);

            return StatusCode(201,response);

        }

    }
}
