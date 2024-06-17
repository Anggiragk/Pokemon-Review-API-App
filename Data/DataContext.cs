using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PokemonApp.Models;
using ReviewApp.Models;

namespace PokemonApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners{ get; set; }
        public DbSet<Pokemon> Pokemons{ get; set; }
        public DbSet<PokemonCategory> PokemonCategories{ get; set; }
        public DbSet<PokemonOwner> PokemonOwners{ get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<Reviewer> Reviewers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonCategory>().HasKey(k => new { k.PokemonId, k.CategoryId});

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.PokemonCategories)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(x => x.Pokemon)
                .WithMany(x => x.PokemonCategories)
                .HasForeignKey(x => x.PokemonId);


            modelBuilder.Entity<PokemonOwner>().HasKey(k => new {k.PokemonId, k.OwnerId});

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.PokemonOwners)
                .HasForeignKey(x => x.OwnerId);

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.PokemonOwners)
                .HasForeignKey(x => x.OwnerId);

            //base.OnModelCreating(modelBuilder);



            //modelBuilder.Entity<Pokemon>()
            //    .HasMany(e => e.Categories)
            //    .WithMany(e => e.Pokemons)
            //    .UsingEntity<PokemonCategory>(
            //        l => l.HasOne<Category>().WithMany(e => e.PokemonCategories),
            //        r => r.HasOne<Pokemon>().WithMany(e => e.PokemonCategories));

            //modelBuilder.Entity<Pokemon>()
            //    .HasMany(e => e.Owners)
            //    .WithMany(e => e.Pokemons)
            //    .UsingEntity<PokemonOwner>(
            //        l => l.HasOne<Owner>().WithMany(e => e.PokemonOwners),
            //        r => r.HasOne<Pokemon>().WithMany(e => e.PokemonOwners));
        }





    }
}
