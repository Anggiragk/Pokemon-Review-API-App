using PokemonApp.DTO;
using PokemonApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryService
    {
        public CategoryResponse GetCategory(int id);
        public List<CategoryResponse> GetAllCategory();
        public List<PokemonResponse> GetPokemonsByCategory(int categoryId);
        public CommonResponse<CategoryResponse> CreateCategory(CategoryRequest categoryRequest);
    }
}
