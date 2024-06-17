using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;

namespace PokemonApp.Services
{
    public class ReviewerService : IReviewerService
    {
        private readonly IReviewerRepository _reviewerRepository;
        public ReviewerService(IReviewerRepository reviewRepository)
        {
            _reviewerRepository = reviewRepository;
        }

        public CommonResponse<ReviewerResponse> createReviewer(ReviewerRequest request)
        {
            var newReviewer = new Reviewer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            var save = _reviewerRepository.CreateReviewer(newReviewer);

            if (save)
            {
                return new CommonResponse<ReviewerResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = Transform.ToReviewerResponse(newReviewer)
                };
            } else
            {
                return new CommonResponse<ReviewerResponse>
                {
                    Status = 400,
                    Message = "Error",
                    Errors = "error, failed create new Reviewer"
                };
            }
        }

        public List<ReviewerResponse> GetAllReviewers()
        {
            List<Reviewer> reviewers = _reviewerRepository.GetAllReviewers();
            List<ReviewerResponse> reviewerResponses = new List<ReviewerResponse>();
            reviewers.ForEach(reviewer =>
            {
                reviewerResponses.Add(Transform.ToReviewerResponse(reviewer));
            });
            return reviewerResponses;
        }

        public ReviewerResponse GetReviewer(int reviewerId)
        {
            Reviewer reviewer = _reviewerRepository.GetReviewer(reviewerId);
            return Transform.ToReviewerResponse(reviewer);
        }

        public List<ReviewResponse> GetReviewsByReviewer(int reviewerId)
        {
            List<Review> reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
            List<ReviewResponse> reviewResponses = new List<ReviewResponse>();
            reviews.ForEach(review =>
            {
                reviewResponses.Add(Transform.ToReviewResponse(review));
            });
            return reviewResponses;
        }
    }
}
