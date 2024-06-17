using PokemonApp.Data;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) 
        {
            _context = context;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool CreateCategory(CategoryRequest request)
        {
            Category category = new Category
            {
                Name = request.Name,
            };
            _context.Add(category);
            return Save();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public List<Pokemon> GetPokemonsByCategory(int categoryId)
        {
           return _context.PokemonCategories.Where(c => c.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool IsCategoryExist(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public Category GetCategory(string categoryName)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == categoryName);
        }
    }
}
