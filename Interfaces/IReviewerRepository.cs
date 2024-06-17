using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface IReviewerRepository
    {
        public List<Reviewer> GetAllReviewers();
        public Reviewer GetReviewer(int reviewerId);
        public List<Review> GetReviewsByReviewer(int reviewerId);
        public bool IsReviewerExist(int reviewerId);
        public bool CreateReviewer(Reviewer reviewer);
        public bool Save();

    }
}
