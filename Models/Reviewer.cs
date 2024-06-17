using System.ComponentModel.DataAnnotations;

namespace PokemonApp.Models
{
    public class Reviewer
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Review> Reviews { get; set; } = []; //= new List<Review>();
    }
}

