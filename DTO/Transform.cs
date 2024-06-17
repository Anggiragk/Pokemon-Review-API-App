using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.DTO
{
    public class Transform
    {
        public static CategoryResponse ToCategoryResponse(Category category)
        {
            return new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static PokemonResponse ToPokemonResponse(Pokemon pokemon, List<Category> pokemonCategories)
        {
            var categories = new List<CategoryResponse>();

            pokemonCategories.ForEach(category => {
                categories.Add(ToCategoryResponse(category));
            });

            return new PokemonResponse
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                BirthDate = pokemon.BirthDate,
                Categories = categories
            };
        }

        public static CountryResponse ToCountryResponse(Country country)
        {
            return new CountryResponse
            {
                Id = country.Id,
                Name = country.Name,
            };
        }

        public static OwnerResponse ToOwnerResponse(Owner owner)
        {
            CountryResponse countryResponse = new CountryResponse
            {
                Id = owner.Country.Id,
                Name = owner.Country.Name,
            };

            return new OwnerResponse
            {
                Id = owner.Id,
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Country = countryResponse,
            };
        }

        public static ReviewResponse ToReviewResponse(Review review)
        {
            return new ReviewResponse
            {
                Id = review.Id,
                Title = review.Title,
                Text = review.Text,
                Rating = review.Rating,
                PokemonName = review.Pokemon.Name,
                ReviewerName = review.Reviewer.FirstName,
            };
        }

        public static ReviewerResponse ToReviewerResponse(Reviewer reviewer)
        {
            return new ReviewerResponse
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName =  reviewer.LastName
            };
                
        }
    }

}
