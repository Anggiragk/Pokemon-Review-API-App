using ReviewApp.Models;

namespace PokemonApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();

    }
}
