using Microsoft.AspNetCore.Mvc;
using PokemonApp.DTO;
using PokemonApp.Interfaces;

namespace PokemonApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewerController : Controller
    {
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IReviewerService _reviewerService;

        public ReviewerController(IReviewerRepository reviewerRepository, IReviewerService reviewerService)
        {
            _reviewerRepository = reviewerRepository;
            _reviewerService = reviewerService;
        }

        [HttpGet]
        public IActionResult GetAllReviewers()
        {
            var responses = _reviewerService.GetAllReviewers();

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(responses);
        }

        [HttpGet("{reviewerId}")]
        public IActionResult GetReviewer(int reviewerId)
        {
            if (!_reviewerRepository.IsReviewerExist(reviewerId))
                return NotFound($"Reviewer with Id {reviewerId} not found");

            var response = _reviewerService.GetReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(response);
        }

        [HttpGet("{reviewerId}/reviews")]
        public IActionResult GetReviewsByReviewer(int reviewerId)
        {
            if (!_reviewerRepository.IsReviewerExist(reviewerId))
                return NotFound($"Reviewer with Id {reviewerId} not found");

            var response = _reviewerService.GetReviewsByReviewer(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CommonResponse<ReviewerResponse>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult createReviewer([FromBody]ReviewerRequest request)
        {
            if (request == null)
                return BadRequest();

            var response = _reviewerService.createReviewer(request);

            if (!ModelState.IsValid)
                return BadRequest();

            return StatusCode(201, response);
        }
    }
}
