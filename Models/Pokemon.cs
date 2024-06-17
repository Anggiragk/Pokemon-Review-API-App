using PokemonApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ReviewApp.Models
{
    public class Pokemon
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Review> Reviews { get; set; } = [];
        public List<PokemonCategory> PokemonCategories { get; set; } = new List<PokemonCategory>();
        public List<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    }
}
