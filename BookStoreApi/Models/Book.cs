using System.Text.Json.Serialization;

namespace BookStoreApi.Models
{
    public class Book
    {
        public string? Id { get; set; }

        [JsonPropertyName("Name")]
        public required string BookName { get; set; }

        public required decimal Price { get; set; }

        public required string Category { get; set; }

        public Author Author { get; set; } = null!;
    }
}
