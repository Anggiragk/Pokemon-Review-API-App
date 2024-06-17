namespace PokemonApp.DTO
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public string PokemonName {  get; set; }
        public string ReviewerName { get; set; }
    }
}
