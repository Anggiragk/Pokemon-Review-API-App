using PokemonApp.Models;

namespace PokemonApp.DTO
{
    public class OwnerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CountryResponse Country { get; set; }
    }
}
