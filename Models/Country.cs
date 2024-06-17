using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Owner> Owners { get; set; } = [];
    }
}
