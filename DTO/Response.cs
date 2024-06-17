namespace PokemonApp.DTO
{
    public class CommonResponse<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Errors;
    }
}
