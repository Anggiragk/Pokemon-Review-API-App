using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewRepository
    {
        public List<Review> GetAllReviews();
        public Review GetReview(int reviewId);
        public List<Review> GetReviewsOfPokemon(int pokemonId);
        public bool IsReviewExist(int reviewId);
        public bool CreateReview(Review review);
        public bool Save();
    }
}
