using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;
        
        public ReviewService(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository)
        {
            _reviewRepository = reviewRepository;
            _reviewerRepository = reviewerRepository;
            _pokemonRepository = pokemonRepository;
        }

        public CommonResponse<ReviewResponse> CreateReview(ReviewRequest request)
        {
            var reviewer = _reviewerRepository.GetReviewer(request.ReviewerId);
            var pokemon = _pokemonRepository.GetPokemon(request.PokemonId);

            var newReview = new Review
            {
                Title = request.Title,
                Text = request.Text,
                Rating = request.Rating,
                Reviewer = reviewer,
                Pokemon = pokemon,
            };

            var save = _reviewRepository.CreateReview(newReview);

            if (save)
            {
                return new CommonResponse<ReviewResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = Transform.ToReviewResponse(newReview)
                };
            }
            else
            {
                return new CommonResponse<ReviewResponse>
                {
                    Status = 400,
                    Message = "Error",
                    Errors = "error, failed create new Review"
                };
            }
        }

        public List<ReviewResponse> GetAllReviews()
        {
            List<Review> reviews =  _reviewRepository.GetAllReviews();
            List<ReviewResponse> reviewResponses = new List<ReviewResponse>();

            reviews.ForEach(r =>
            {
                reviewResponses.Add(Transform.ToReviewResponse(r));
            });
            return reviewResponses;
        }

        public ReviewResponse GetReview(int id)
        {
            Review review = _reviewRepository.GetReview(id);
            return Transform.ToReviewResponse(review);
        }

        public List<ReviewResponse> GetReviewsOfPokemon(int pokemonId)
        {
            List<Review> reviews = _reviewRepository.GetReviewsOfPokemon(pokemonId);
            List<ReviewResponse> reviewResponses = new List<ReviewResponse>();
            reviews.ForEach(r =>
            {
                reviewResponses.Add(Transform.ToReviewResponse(r));
            });
            return reviewResponses;
        }
    }
}
