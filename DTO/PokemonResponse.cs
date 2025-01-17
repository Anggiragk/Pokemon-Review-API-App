﻿namespace PokemonApp.DTO
{
    public class PokemonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<CategoryResponse> Categories { get; set; }
    }
}
