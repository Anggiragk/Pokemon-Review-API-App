using PokemonApp.Models;
using ReviewApp.Models;
using System.ComponentModel.DataAnnotations;
namespace PokemonApp.Models
{
    public class Owner
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public Country Country { get; set; }
        public List<PokemonOwner> PokemonOwners { get; set; } = new List<PokemonOwner>();
    }
}
