using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewService _reviewService;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public ReviewController(IReviewRepository reviewRepository, IReviewService reviewService, IPokemonRepository pokemonRepository, IReviewerRepository reviewerRepository)
        {
            _reviewRepository = reviewRepository;
            _reviewService = reviewService;
            _pokemonRepository = pokemonRepository;
            _reviewerRepository = reviewerRepository;
        }

        [HttpGet]
        public IActionResult GetAllReviews()
        {
            List<ReviewResponse> responses = _reviewService.GetAllReviews();
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(responses);
        }

        [HttpGet("{reviewId}")]
        public IActionResult GetReview(int reviewId)
        {
            if (!_reviewRepository.IsReviewExist(reviewId))
                return NotFound($"Review with id {reviewId} not found");

            var response = _reviewService.GetReview(reviewId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(response);

        }

        [HttpGet("pokemon/{pokemonId}")]
        public IActionResult GetReviewsOfPokemon(int pokemonId)
        {
            if (!_pokemonRepository.IsPokemonExist(pokemonId))
                return NotFound($"Pokemon with id {pokemonId} not found");

            var responses = _reviewService.GetReviewsOfPokemon(pokemonId);
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok(responses);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CommonResponse<ReviewResponse>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult createReview([FromBody] ReviewRequest request)
        {
            if (request == null)
                return BadRequest();

            if (!_reviewerRepository.IsReviewerExist(request.ReviewerId))
                return NotFound("Reviewer Not found");

            if (!_pokemonRepository.IsPokemonExist(request.PokemonId))
                return NotFound("Pokemon not found");

            var response = _reviewService.CreateReview(request);

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(response.Status, response);
        }
    }
}