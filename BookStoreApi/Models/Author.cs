namespace BookStoreApi.Models
{
    public class Author
    {
        public string Id { get; set; }
        public required string? FirstName { get; set; }
        public required string? LastName { get; set; }
    }
}
