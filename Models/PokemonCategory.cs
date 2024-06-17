using ReviewApp.Models;
using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class PokemonCategory
    {
        [Key]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        [Key]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
