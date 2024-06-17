using PokemonApp.DTO;

namespace PokemonApp.Interfaces
{
    public interface IReviewService
    {
        public List<ReviewResponse> GetAllReviews();
        public ReviewResponse GetReview(int id);
        public List<ReviewResponse> GetReviewsOfPokemon(int pokemonId);
        public CommonResponse<ReviewResponse> CreateReview(ReviewRequest request);
    }
}
