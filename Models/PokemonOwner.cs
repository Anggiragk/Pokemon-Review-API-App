using ReviewApp.Models;
using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class PokemonOwner
    {
        [Key]
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }

        [Key]
        public int OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
