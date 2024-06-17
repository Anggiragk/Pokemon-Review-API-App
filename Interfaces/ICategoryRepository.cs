using PokemonApp.DTO;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetCategory(int id);
        public Category GetCategory(string categoryName);
        public List<Pokemon> GetPokemonsByCategory(int categoryId);
        public bool IsCategoryExist(int id);
        public bool CreateCategory (CategoryRequest category);
        bool Save();
    }
}
