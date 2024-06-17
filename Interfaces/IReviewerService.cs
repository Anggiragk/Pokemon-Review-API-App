using PokemonApp.DTO;

namespace PokemonApp.Interfaces
{
    public interface IReviewerService
    {
        public List<ReviewerResponse> GetAllReviewers();
        public ReviewerResponse GetReviewer(int reviewerId);
        public List<ReviewResponse> GetReviewsByReviewer(int reviewerId);
        public CommonResponse<ReviewerResponse> createReviewer(ReviewerRequest request);
    }
}
