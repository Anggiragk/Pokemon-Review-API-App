using Microsoft.AspNetCore.Http.HttpResults;
using PokemonApp.DTO;
using PokemonApp.Interfaces;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPokemonRepository _pokemonRepository;

        public CategoryService(ICategoryRepository categoryRepository, IPokemonRepository pokemonRepository) {
            this._categoryRepository = categoryRepository;
            this._pokemonRepository = pokemonRepository;
        }

        public CommonResponse<CategoryResponse> CreateCategory(CategoryRequest createCategory)
        {
            if (_categoryRepository.GetCategory(createCategory.Name) != null)
            {
                return new CommonResponse<CategoryResponse>
                {
                    Status = 400,
                    Message = "failed to save, category already exist",
                    Errors = "true"
                };
            }
                
            var save = _categoryRepository.CreateCategory(createCategory);
            if (save)
            {
                var category = _categoryRepository.GetCategory(createCategory.Name);
                return new CommonResponse<CategoryResponse>
                {
                    Status = 201,
                    Message = "Success",
                    Data = Transform.ToCategoryResponse(category)
                };
            }
            else
            {
                return new CommonResponse<CategoryResponse>
                {
                    Status = 400,
                    Message = "Failed",
                    Errors = "true"
                };
            }
        }

        public List<CategoryResponse> GetAllCategory()
        {
            List<Category> categories = _categoryRepository.GetCategories();
            List<CategoryResponse> categoryResponses = new List<CategoryResponse>();
            categories.ForEach(c =>
            {
                categoryResponses.Add(Transform.ToCategoryResponse(c));
            });
            return categoryResponses;
        }

        public CategoryResponse GetCategory(int id)
        {
            Category category = _categoryRepository.GetCategory(id);
            return Transform.ToCategoryResponse(category);
        }

        public List<PokemonResponse> GetPokemonsByCategory(int categoryId)
        {
            List<Pokemon> pokemons = _categoryRepository.GetPokemonsByCategory(categoryId);
            List<PokemonResponse> pokemonResponses = new List<PokemonResponse>();

            pokemons.ForEach(p => {
                var categories = _pokemonRepository.GetCategoriesByPokemon(p.Id);

                pokemonResponses.Add(Transform.ToPokemonResponse(p, categories));
            });
            return pokemonResponses;
        }
    }
}
