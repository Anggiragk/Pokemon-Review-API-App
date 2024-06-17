using PokemonApp.Data;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
           
        }
    }
}